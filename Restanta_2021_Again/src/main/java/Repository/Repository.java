package Repository;

import domain.VacationDestination;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class Repository {
    private static final String database = "Restanta_2021";
    private static final String user = "postgres";
    private static final String password = "branza123";
    private static final String connectionString = "jdbc:postgresql://localhost:5432/" + database;

    public Repository() {
        try {
            Class.forName("org.postgresql.Driver");
        } catch (ClassNotFoundException exception) {
            exception.printStackTrace();
        }
    }

    private static void executeStatement(String statement) {
        try (Connection connection = DriverManager.getConnection(connectionString, user, password);
             PreparedStatement preparedStatement = connection.prepareStatement(statement)) {
            preparedStatement.execute();
        } catch (SQLException sqlException) {
            sqlException.printStackTrace();
        }
    }

    public void addBannedList(String user, int destinationId) {
        executeStatement("INSERT INTO BannedList(username, destinationid) VALUES (\'" + user + "\', " + destinationId + ")");
    }

    public List<VacationDestination> getLikeDestination(String destination, String username) {
        List<VacationDestination> result = new ArrayList<>();
        String statement = "SELECT * FROM VacationDestionation V WHERE V.destination LIKE \'%" + destination + "%\' AND V.id NOT IN (SELECT B.destinationid FROM BannedList B WHERE B.username = \'" + username + "\')";
        try (Connection connection = DriverManager.getConnection(connectionString, user, password);
             PreparedStatement preparedStatement = connection.prepareStatement(statement);
             ResultSet resultSet = preparedStatement.executeQuery()) {
            while (resultSet.next())
                result.add(new VacationDestination(
                        resultSet.getInt(1),
                        resultSet.getString(2),
                        resultSet.getString(3),
                        resultSet.getInt(4)
                ));
        } catch (SQLException sqlException) {
            sqlException.printStackTrace();
        }
        return result;
    }

}
