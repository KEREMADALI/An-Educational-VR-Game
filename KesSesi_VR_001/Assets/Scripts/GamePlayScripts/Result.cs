using System.Collections.Generic;

public class Result {

    private int id;
    private int target;
    private int successCount;
    private  int failCount;
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

    public List<bool> getResultOrder() {
        return resultOrder;
    } 

    public void update(bool isHit) {
        if (isHit)
            successCount++;
        else
            failCount++;

        resultOrder.Add(isHit);
    }

    public void reset() {
        successCount = 0;
        failCount = 0;
    }

}
