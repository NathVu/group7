import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.geometry.Orientation;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.layout.VBox;
import javafx.scene.layout.HBox;
import javafx.stage.Stage;
import javafx.scene.control.TextField;
import javafx.scene.text.*;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.ScrollBar;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import java.io.IOException;
import java.sql.*;
import java.util.Properties;
import javafx.scene.control.cell.PropertyValueFactory;

public class GUI extends Application {

    private TableView table = new TableView();

    public void start(Stage primaryStage) {

        Text title = new Text("☏ 311 CALLS MANAGER ☏");
        title.setId("title");
        Text te1 = new Text("From(HH:MM):");
        Text te2 = new Text("To(HH:MM):");
        Text te3 = new Text("ID:");
        Text te4 = new Text("Date(MM/DD/YYYY):");

        TextField tf1 = new TextField("");
        TextField tf2 = new TextField("");
        TextField tf3 = new TextField("");
        TextField tf4 = new TextField("");
        addTextLimiter(tf1,5);
        addTextLimiter(tf2,5);
        addTextLimiter(tf3,8);
        addTextLimiter(tf4,10);

        Button btn1 = new Button("Search");
        btn1.setOnAction(new EventHandler<ActionEvent>() {
            public void handle(ActionEvent event) {
                String date = null;
                String startTime = null;
                String endTime = null;
                String id = null;
                if (!tf1.getText().equals(""))
                    date = tf1.getText();
                if (!tf2.getText().equals(""))
                    startTime = tf2.getText();
                if (!tf3.getText().equals(""))
                    endTime = tf3.getText();
                if (!tf4.getText().equals(""))
                    id = tf4.getText();
            }
        });

        Button btn2 = new Button("Reset");
        btn2.setId("Reset");
        btn2.setOnAction(new EventHandler<ActionEvent>() {
            public void handle(ActionEvent event) {

            }
        });

        table.setEditable(false);

        TableColumn idCol = new TableColumn("ID");
        idCol.setMinWidth(100);
        idCol.setCellFactory(new PropertyValueFactory<>("id"));

        TableColumn openCol = new TableColumn("Open Date and Time");
        openCol.setMinWidth(150);
        openCol.setCellFactory(new PropertyValueFactory<>("open"));

        TableColumn closeCol = new TableColumn("Closed Date and Time");
        closeCol.setMinWidth(175);
        closeCol.setCellFactory(new PropertyValueFactory<>("close"));

        TableColumn typeCol = new TableColumn("Type of Call");
        typeCol.setMinWidth(175);
        typeCol.setCellFactory(new PropertyValueFactory<>("type"));

        TableColumn addCol = new TableColumn("Address");
        addCol.setCellFactory(new PropertyValueFactory<>("add"));
        TableColumn streetCol = new TableColumn("Street");
        streetCol.setMinWidth(200);
        streetCol.setCellFactory(new PropertyValueFactory<>("street"));

        TableColumn zipCol = new TableColumn("Zip Code");
        zipCol.setMinWidth(100);
        addCol.getColumns().addAll(streetCol,zipCol);
        zipCol.setCellFactory(new PropertyValueFactory<>("zip"));

        TableColumn depCol = new TableColumn("Department");
        depCol.setMinWidth(100);
        depCol.setCellFactory(new PropertyValueFactory<>("dep"));

        TableColumn statCol = new TableColumn("Status");
        statCol.setMinWidth(100);
        statCol.setCellFactory(new PropertyValueFactory<>("stat"));

        table.getColumns().addAll(idCol,openCol,closeCol,depCol,typeCol,addCol,statCol);

        HBox top1 = new HBox(5);
        top1.getChildren().addAll(te4,tf4,te1,tf1,te2,tf2,te3,tf3,btn1,btn2);
        top1.setAlignment(Pos.CENTER);
        top1.setSpacing(10);

        HBox top2 = new HBox(1);
        top2.getChildren().addAll(table);
        top2.setAlignment(Pos.CENTER);

        VBox root = new VBox();
        root.getChildren().addAll(new HBox(),title,top1,top2);
        root.setSpacing(40);
        root.setAlignment(Pos.TOP_CENTER);
	    
	ScrollBar vscroll = new ScrollBar();
        vscroll.setMin(0);
        vscroll.setMax(250);
        vscroll.setValue(100);
        vscroll.setOrientation(Orientation.VERTICAL);
        vscroll.setTranslateY(20);
        root.getChildren().addAll(vscroll);

        Scene scene = new Scene(root,1300,800);
        scene.getStylesheets().add("GUI_CSS_Format.css");

        primaryStage.setTitle("311 Calls");
        primaryStage.setScene(scene);
        primaryStage.show();

    }
    public static void addTextLimiter(final TextField tf, final int maxLength) {
        tf.textProperty().addListener(new ChangeListener<String>() {
            @Override
            public void changed(final ObservableValue<? extends String> ov, final String oldValue, final String newValue) {
                if (tf.getText().length() > maxLength) {
                    String s = tf.getText().substring(0, maxLength);
                    tf.setText(s);
                }
            }
        });
    }
    public static void main(String[] args) throws Exception{
        try {
		    /*String databaseName = "postgres";

		    Class.forName("org.postgresql.Driver");

		    String username = "ivan";
		    String password = "NT0408";

		    String url = "jdbc:postgresql://35.193.33.89/group7";

		    Connection connection = DriverManager.getConnection(url, username, password);*/
            String url = "jdbc:postgresql://35.193.33.89/group7?user=ivan&password=NT0408&ssl=true";
            Connection conn = DriverManager.getConnection(url);
        }
        catch(Exception e){
            System.out.println(e);
        }
        launch(args);
    }
}
