
import java.sql.*;

public class DbConnector {
    
    private Connection con;
    private Statement st;
    private ResultSet rs;
    
    public DbConnector()
    {
     try {
        Class.forName("com.postgresql.jdbc.Driver");
        con = DriverManager.getConnection("jdbc:postgresql://localhost:5432/test","","");
        st = con.createStatement();
     }catch(Exception ex){
      System.out.println();
     }
    
    }
    public void getData(){
    try{
      String query = "select * from persons";
      rs=st.executeQuery(query);
      System.out.println("Record from data");
      while (rs.next()){
       String name = rs.getString("name");
       String age= rs.getString("age");
       System.out.println("name is  "+name + "age is " + age);
           
      
      }
    }catch(Exception ex){
    System.out.println();   
     }
       
    
    }
}
