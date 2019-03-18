import javafx.application.Application;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.scene.Scene;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;

public class Main extends Application {

    Stage pStage;
    TableView<History> table;

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage primaryStage) throws Exception {
        pStage = primaryStage;
        pStage.setTitle("Daily 311 Service Phone Calls");

        //Unique key column
        TableColumn<History, Double> uniqueKeyColumn = new TableColumn<>("Unique Key");
        uniqueKeyColumn.setMinWidth(200);
        uniqueKeyColumn.setCellValueFactory(new PropertyValueFactory<>("unique"));

        //Created date column
        TableColumn<History, String> createdDateColumn = new TableColumn<>("Created Date");
        createdDateColumn.setMinWidth(200);
        createdDateColumn.setCellValueFactory(new PropertyValueFactory<>("createdDate"));

        //Closed date column
        TableColumn<History, String> closedDateColumn = new TableColumn<>("Closed Date");
        closedDateColumn.setMinWidth(200);
        closedDateColumn.setCellValueFactory(new PropertyValueFactory<>("closedDate"));

        //Agency column
        TableColumn<History, String> agencyColumn = new TableColumn<>("Agency");
        agencyColumn.setMinWidth(200);
        agencyColumn.setCellValueFactory(new PropertyValueFactory<>("agency"));

        //Type of complaint column
        TableColumn<History, String> typeOfComplaintColumn = new TableColumn<>("Type of Complaint");
        typeOfComplaintColumn.setMinWidth(200);
        typeOfComplaintColumn.setCellValueFactory(new PropertyValueFactory<>("typeOfComplaint"));

        //Location column
        TableColumn<History, String> locationColumn = new TableColumn<>("Location");
        locationColumn.setMinWidth(200);
        locationColumn.setCellValueFactory(new PropertyValueFactory<>("location"));

        table = new TableView<>();
        table.setItems(getHistory());
        table.getColumns().addAll(uniqueKeyColumn, createdDateColumn, closedDateColumn, agencyColumn,
                typeOfComplaintColumn, locationColumn);

        VBox vBox = new VBox();
        vBox.getChildren().addAll(table);

        Scene scene = new Scene(vBox);
        pStage.setScene(scene);
        pStage.show();
    }

    public ObservableList<History> getHistory(){
        ObservableList<History> history = FXCollections.observableArrayList();
        history.add(new History());
        return history;
    }


}
