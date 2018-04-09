using System.Collections.Generic;
using System;

[Serializable]
public class Result {

    private int id;
    [NonSerialized]
    private int target;
    private int successCount;
    private int failCount;
    [NonSerialized]
    private List<bool> resultOrder;

    public Result(int _id) {
        id = _id;
        successCount = 0;
        failCount = 0;
        resultOrder = new List<bool>();
    }

    public int getId() {
        return id;
    }

    public int getSuccessCount() {
        return successCount;
    }

    public int getFailCount() {
        return failCount;
    }

    public List<bool> getResultOrder() {
        return resultOrder;
    } 

    public void update(bool isHit) {
        if (isHit)
            successCount++;
        else
            failCount++;

        if(resultOrder == null)
            resultOrder = new List<bool>();

        resultOrder.Add(isHit);
    }

    public void reset() {
        successCount = 0;
        failCount = 0;

        if(resultOrder != null)
            resultOrder.Clear();
    }

}
