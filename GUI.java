import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
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
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn.CellDataFeatures;
public class GUI extends Application{

    private TableView table = new TableView();

    public void start(Stage primaryStage) {
    	
    	Text title = new Text("311 Calls Manager");
    	title.setId("title");
        Text te1 = new Text("From (HH:MM):");
        Text te2 = new Text("To (HH:MM):");
        Text te3 = new Text("ID:");
        Text te4 = new Text("Date (MM/DD/YYYY)");

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

            }
        });

        table.setEditable(false);

        TableColumn idCol = new TableColumn("ID");
        idCol.setMinWidth(100);
     
        TableColumn dateCol = new TableColumn("Open Date and Time");
        dateCol.setMinWidth(150);

        TableColumn closeCol = new TableColumn("Closed Date and Time");
        closeCol.setMinWidth(175);

        TableColumn typeCol = new TableColumn("Type of Call");
        typeCol.setMinWidth(175);

        TableColumn addCol = new TableColumn("Address");
        TableColumn streetCol = new TableColumn("Street");
        streetCol.setMinWidth(200);
        TableColumn zipCol = new TableColumn("Zip Code");
        zipCol.setMinWidth(100);
        addCol.getColumns().addAll(streetCol,zipCol);

        TableColumn depCol = new TableColumn("Department");
        depCol.setMinWidth(100);

        TableColumn statCol = new TableColumn("Status");
        statCol.setMinWidth(100);

        table.getColumns().addAll(idCol,dateCol,closeCol,depCol,typeCol,addCol,statCol);

        HBox top1 = new HBox(5);
        top1.getChildren().addAll(te4,tf4,te1,tf1,te2,tf2,te3,tf3,btn1);
        top1.setAlignment(Pos.CENTER);
        top1.setSpacing(10);

        HBox top2 = new HBox(1);
        top2.getChildren().addAll(table);
        top2.setAlignment(Pos.CENTER);

        VBox root = new VBox();
        root.getChildren().addAll(new HBox(),title,top1,top2);
        root.setSpacing(10);
        root.setAlignment(Pos.TOP_CENTER);
        
        ObservableList<ObservableList> csvData = FXCollections.observableArrayList();
        
        Scene scene = new Scene(root,1200,700);
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
    public static void main(String[] args) {
        launch(args);
    }
}
