
public class Result {

    private int target;
    private int successCount;
    private int failCount;

    public Result() {
        successCount = 0;
        failCount = 0;
    }

    public void hit(){
        successCount++;
    }

    public void miss() {
        failCount++;
    }

    public void reset() {
        successCount = 0;
        failCount = 0;
    }

}
