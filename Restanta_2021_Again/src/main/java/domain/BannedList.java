package domain;

public class BannedList {
    int id;
    String user;
    int destinationId;

    public BannedList(int id, String user, int destinationId) {
        this.id = id;
        this.user = user;
        this.destinationId = destinationId;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getUser() {
        return user;
    }

    public void setUser(String user) {
        this.user = user;
    }

    public int getDestinationId() {
        return destinationId;
    }

    public void setDestinationId(int destinationId) {
        this.destinationId = destinationId;
    }
}
