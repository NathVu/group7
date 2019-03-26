
import java.sql.*;

public class DbConnector 
{
    
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
   
    public int uniquekey() throws SQLException
    {
     String query = "select * totalrequest";
     rs=st.executeQuery(query);
     int uniquekey = rs.getInt("unique_key");
        return uniquekey;
    }
    public String agency() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String Agency= rs.getString("agency");
     return Agency;
    }
    public String agencyname() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String AgencyName= rs.getString("agency");
     return AgencyName;
    }
    public String complaintype() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String complain= rs.getString("complain_type");
     return complain;
    }
    public String descriptor() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String des= rs.getString("descriptor");
     return des;
    }
    public String locationtype() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String loc= rs.getString("location_typr");
     return loc;
    }
    public String incidentzip() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String inzip= rs.getString("incident_zip");
     return inzip;
    }
    public String incidentadd() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String incadd= rs.getString("incident_address");
     return incadd;
    }
    public String streetname() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String stname= rs.getString("street_name");
     return stname;
    }
    public String crossst1() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String crossstone= rs.getString("cross_street_1");
     return crossstone;
    }
         public String crossst2() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String crosssttwo = rs.getString("cross_street_2");
     return crosssttwo;
    }
    
      public String intersection1() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String interset1 = rs.getString("intersection_street_1");
     return interset1;
    }
      public String intersection2() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String intersec2 = rs.getString("intersection_street_2");
     return intersec2;
    }
       public String addresstype() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String addresstype = rs.getString("address_type");
     return addresstype;
    }
       public String city() throws SQLException
    {
      String query = "select * totalrequest";
     rs=st.executeQuery(query);
     String city = rs.getString("city");
     return city;
    }
}
