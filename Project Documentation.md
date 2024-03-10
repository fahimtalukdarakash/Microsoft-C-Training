# ML 23/24-03 Implement the new Spatial Learning experiment
Group Name: Team_SqrAs [Main Branch](https://github.com/fahimtalukdarakash/neocortexapi_Team_SqrAS)

## Project Description:
The current experiment runs forever (maxSPLearningCycles). The new code should persist SDRs of all inputs in the dictionary, like
Dictionary<input, sdr> -> Dictionary<int, List<int>>
Input is the key, which is the scalar value. The value in the dictionary is the list of integers that represent the SDR (currently active mini-columns). During learning time, the SP will create different SDRs for the same input. After a while, it will keep the SDR stable.
Your code should ensure that, after the variable isInStableState is set to true, NO SDR change is detected.
To solve this, you might track the last iteration (cycle) in which the SDR of the certain input was set.
The experiment should exit after the SDR does not change for the given number of iterations.
Please also find a better way to output the result. For example,
Cycle N – How many iterations the SDR for the input was not changes (is stable)
instead of
[cycle=0419, i=46, cols=:20 s=100] SDR: 357, 362, 363, 379, 391
Use an additional value stable X cycles.
[cycle=0419,N= 10, i=46, cols=:20 s=100, stable 7 cycles] SDR: 357, 362, 363, 379, 391

## 1. Objectives:
- **Task 1:** Analyzing the code of spatial pattern learning
- **Task 2:** Explaining the tasks of spatial learning process in MD file
- **Task 3:** Investing the reason, why first 40 cycles don't give mini columns list for inputs 51 to 99.
- **Task 4:** Implementing new spatial pattern learning experiment.
- **Task 5:** Write out the dictionary at the end
- **Task 6:** Provide some more readable statistical info about the stability of all mini-columns.

## Approach

### Task 1: Analyzing the code of spatial pattern learning
### Task 2: Explaining the tasks of spatial learning process in MD file
### Task 3: Investegating the reason, why first 40 cycles don't give mini columns list for inputs 51 to 99.
At first, we thought the problem is in HomeostaticPlasticityController. Then we ran the program without without HomeostaticPlasticityController. We found that the problem is not there.
Then we tried to understand the Spatial Pooler Algorithm to find out the problem. We understood that the inhibition algorithm is the reason for inhibiting mini columns SDR list. In this experiment for Spatial Pattern Learning Experiment, the local inhibition was used. We saw that there are in total 6 different local inhibition algorithm implemented.

```
public virtual int[] InhibitColumnsLocal(Connections c, double[] overlaps, double density)
{
    return InhibitColumnsLocalOriginal(c, overlaps, density);
    //return InhibitColumnsLocalNewApproach(c, overlaps);
    //return inhibitColumnsLocalNewApproach2(c, overlaps, density);
    //return inhibitColumnsLocalNewApproach3(c, overlaps, density);
    //return InhibitColumnsLocalNewApproach11(c, overlaps, density);
    //return InhibitColumnsLocalNew(c, overlaps, density);
}

```
We tried all the algorithms one by one.
 **InhibitColumnsLocalNewApproach(c, overlaps)** this function doesn't take density into account and gives error when there is no  columns for input 51. **inhibitColumnsLocalNewApproach2(c, overlaps, density)** this fuction generates all the  columns for input 0 from the beginning. For the first few inputs, it generates too much  columns and after few inputs, the other input's  columns are low and from this function the  columns SDR list are not coming from input 51 to input 99. After cycle 41, it generates a lot of  columns instead of **0.02*numColumns**.**inhibitColumnsLocalNewApproach3(c, overlaps, density)** this function doesn't generate any  columns for the inputs. **InhibitColumnsLocalNewApproach11(c, overlaps, density)** this function generates  columns from the beginning for all the inputs but the main problem is, the columns SDR list length is also a lot instead of `0.02*numColumns`. **InhibitColumnsLocalNew(c, overlaps, density)** it also behaves like **inhibitColumnsLocalNewApproach2(c, overlaps, density)** this function. The best one is **InhibitColumnsLocalOriginal(c, overlaps, density)** because it generates `0.02*numColumns` length of  columns per input for all the inputs after 40 cycles. But the problem of not generating columns for input 51 to input 99 for the first 40 cycles was still there. 
As we tried all the inhibition algorithms and found that the problem is not there because inhibition algorithm inhibits columns when there will be minimum one column for one input. So if there are no columns for one input then it will not inhibit the columns for that particular input. So those inputs that don't generate columns that means the columns is not strong enough for the inputs. So the problem is in the boosting algorithm where the columns are boosted. 
So we tried understand the function where the boosting of low overlap columns are happening. Here is the function:
```
 public virtual void BoostColsWithLowOverlap(Connections c)
 {
     // Get columns with too low overlap.
     var weakColumns = c.Memory.Get1DIndexes().Where(i => c.HtmConfig.OverlapDutyCycles[i] < c.HtmConfig.MinOverlapDutyCycles[i]).ToArray();

     for (int i = 0; i < weakColumns.Length; i++)
     {
         Column col = c.GetColumn(weakColumns[i]);

         Pool pool = col.ProximalDendrite.RFPool;
         double[] perm = pool.GetSparsePermanences();
         ArrayUtils.RaiseValuesBy(c.HtmConfig.SynPermBelowStimulusInc, perm);
         int[] indexes = pool.GetSparsePotential();

         col.UpdatePermanencesForColumnSparse(c.HtmConfig, perm, indexes, true);
         //UpdatePermanencesForColumnSparse(c, perm, col, indexes, true);
     }
 }
```
So, our idea is to reducing the number weak columns as fast as possible. So after getting the weak columns, we tried to implement another algorithm. We read the paper [The HTM Spatial Pooler—A Neocortical Algorithm for Online Sparse Distributed Coding](https://www.frontiersin.org/articles/10.3389/fncom.2017.00111/full#B14) written by Yuwei Cui, Subutai Ahmad, Jeff hawkins. There we found a mathematical equation of boosting the weak columns. 
![Equation of Boosting Algorithm](D:\Information Technology\Equation of Boosting.png)
So, we implement this equation as a new boosting algorithm
```
public virtual void BoostColsWithLowOverlap2(Connections c)
{
    // Get columns with too low overlap.
    var weakColumns = c.Memory.Get1DIndexes().Where(i => c.HtmConfig.OverlapDutyCycles[i] < c.HtmConfig.MinOverlapDutyCycles[i]).ToArray();

    foreach (var weakColumnIndex in weakColumns)
    {
        Column col = c.GetColumn(weakColumnIndex);

        // Get the boosting signal for the weak column based on its overlap duty cycle.
        double boostingSignal = CalculateBoostingSignal(col, c.HtmConfig);

        // Adjust the synaptic connections (permanences) associated with the weak column.
        AdjustPermanencesForColumn(col, c.HtmConfig.SynPermBelowStimulusInc, boostingSignal,c);
    }
}

private double CalculateBoostingSignal(Column col, HtmConfig config)
{
    double overlapDutyCycle = config.OverlapDutyCycles[col.Index];
    double minOverlapDutyCycle = config.MinOverlapDutyCycles[col.Index];
    double BoostAlpha = 100;
    // Calculate the boosting signal based on the difference between the current overlap duty cycle and the minimum overlap duty cycle.
    //double boostingSignal = 1 / (1 + Math.Exp(-BoostAlpha * (overlapDutyCycle - minOverlapDutyCycle)));
    double boostingSignal = Math.Exp(-BoostAlpha * (overlapDutyCycle - minOverlapDutyCycle));

    return boostingSignal;
}

private void AdjustPermanencesForColumn(Column col, double synPermBelowStimulusInc, double boostingSignal, Connections c)
{
    Pool pool = col.ProximalDendrite.RFPool;
    double[] permanences = pool.GetSparsePermanences();

    // Boost the permanences associated with the weak column.
    for (int i = 0; i < permanences.Length; i++)
    {
        permanences[i] += synPermBelowStimulusInc * boostingSignal;
    }

    // Update the permanences for the column.
    col.UpdatePermanencesForColumnSparse(c.HtmConfig, permanences, pool.GetSparsePotential(), true);
}

```
After implementing this, we ran the program but we didn't see any sigficant changes. 
Then we assumed that specifically the problem is maybe in this line:
```
var weakColumns = c.Memory.Get1DIndexes().Where(i => c.HtmConfig.OverlapDutyCycles[i] < c.HtmConfig.MinOverlapDutyCycles[i]).ToArray();
```
In this line code, if OverlapDutyCycles[i] is less than MinOverlapDutyCycles[i] then weak column will be detected. So, if we can increase the value of OverlapDutyCycles[i] as well as decrease the value of MinOverlapDutyCycles[i] then the weak columns will be reduced faster than before. So we saw from where OverlapDutyCycles are calcluated. OverlapDutyCycles values are coming from this function: 
```
public void UpdateDutyCycles(Connections c, int[] overlaps, int[] activeColumns)
{
    // All columns with overlap are set to 1. Otherwise 0.
    double[] overlapFrequencies = new double[c.HtmConfig.NumColumns];

    // All active columns are set on 1, otherwise 0.
    double[] activeColFrequencies = new double[c.HtmConfig.NumColumns];

    //
    // if (sourceA[i] > 0) then targetB[i] = 1;
    // This ensures that all values in overlapCycles are set to 1, if column has some overlap.
    ArrayUtils.GreaterThanXThanSetToYInB(overlaps, overlapFrequencies, 0, 1);

    if (activeColumns.Length > 0)
    {
        // After this step, all rows in activeCycles are set to 1 at the index of active column.
        ArrayUtils.SetIndexesTo(activeColFrequencies, activeColumns, 1);
    }

    int period = c.HtmConfig.DutyCyclePeriod;
    if (period > c.SpIterationNum)
    {
        period = c.SpIterationNum;
    }

    c.HtmConfig.OverlapDutyCycles = CalcEventFrequency(c.HtmConfig.OverlapDutyCycles, overlapFrequencies, period);

    c.HtmConfig.ActiveDutyCycles = CalcEventFrequency(c.HtmConfig.ActiveDutyCycles, activeColFrequencies, period);
}

```
So, from this function, we can see that if we increase the value of the variable 'period' then value of OverlapDutyCycles[i] will be increased. The value of the variable 'period' is coming from `HtmConfig.DutyCyclePeriod`. So if we increase the value of the variable `DutyCyclePeriod` then the OverlapDutyCycles[i] will be increased. 
The other thing is, we have to decrease the value of MinOverlapDutyCycles[i]. In the function below is calculating the MinOverlapDutyCycles[i]: 
```
public void UpdateMinDutyCyclesLocal(Connections c)
{
    int len = c.HtmConfig.NumColumns;

    Parallel.For(0, len, (i) =>
    {
        int[] neighborhood = GetColumnNeighborhood(c, i, this.InhibitionRadius);

        double maxActiveDuty = ArrayUtils.Max(ArrayUtils.ListOfValuesByIndicies(c.HtmConfig.ActiveDutyCycles, neighborhood));
        double maxOverlapDuty = ArrayUtils.Max(ArrayUtils.ListOfValuesByIndicies(c.HtmConfig.OverlapDutyCycles, neighborhood));

        c.HtmConfig.MinActiveDutyCycles[i] = maxActiveDuty * c.HtmConfig.MinPctActiveDutyCycles;

        c.HtmConfig.MinOverlapDutyCycles[i] = maxOverlapDuty * c.HtmConfig.MinPctOverlapDutyCycles;
    });
}

```
From this function, we are seeing that MinOverlapDutyCycles[i] is calculated by multiplying maxOverlapDuty and MinPctOverlapDutyCycles. The value of MinPctOverlapDutyCycles is coming from HtmConfig. So if we decrease the value of MinPctOverlapDutyCycles then the value of MinOverlapDutyCycles[i] will be decreased also. 
So, we tuned the value of `DutyCyclePeriod` and `MinPctOverlapDutyCycles` and our assumption was right. We are getting the value from cycle 2 to cycle 3 for all the inputs. The observation of tuning the parameters is given below sections and here we are giving the only one picture where we got the best output. Because we are getting the value from cycle 3 as well as getting the stability faster comparing to other parameter tuning. The updated value of these two variables are `DutyCyclePeriod = 1000` and `MinPctOverlapDutyCycles = MinOctOverlapCycles` where `MinOctOverlapCycles = 0.45`. By using these value we are getting the stability on cycle 301. Here are the figures:![Getting the SDRs of input 51 to input 99 from cycle 3](C:\Users\USER\Desktop\from Cycle 3.png) ![Getting the stability on cycle 301](C:\Users\USER\Desktop\Stable on 301 cycle).
### Task 4: Implementing new spatial pattern learning experiment

