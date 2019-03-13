import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.layout.VBox;
import javafx.scene.layout.HBox;
import javafx.scene.transform.Scale;
import javafx.stage.Stage;
import javafx.scene.layout.StackPane;
import javafx.scene.control.TextField;
import javafx.scene.text.*;
import javax.swing.JTextField;
import javafx.scene.Group;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.paint.Color;
public class GUI extends Application{
	 private TableView table = new TableView();
	    public void start(Stage primaryStage) {
	        Text te1 = new Text("from:");
	        Text te2 = new Text("to:");
	        TextField t1 = new TextField("");
	        TextField t2 = new TextField("");
	        Button btn1 = new Button("Search");
	        btn1.setOnAction(new EventHandler<ActionEvent>() {
	            public void handle(ActionEvent event) {
	            	
	            }
	        });
	        table.setEditable(false);
	        TableColumn dateCol = new TableColumn("Date");
	        TableColumn timeCol = new TableColumn("Time");
	        TableColumn typeCol = new TableColumn("Type of Call");
	        table.getColumns().addAll(dateCol, timeCol, typeCol);
	        HBox top = new HBox(5);
	        top.getChildren().addAll(te1,t1,te2,t2,btn1);
	        VBox buttons = new VBox(4);
	        top.setAlignment(Pos.CENTER);
	        buttons.getChildren().addAll(top,table);
	        
	        
	        Scene scene = new Scene(buttons, 900, 750);
	        
	        primaryStage.setTitle("311 Calls");
	        primaryStage.setScene(scene);
	        primaryStage.show();
	    }
	    public static void main(String[] args) {
	        launch(args);
	    }
	    
}
