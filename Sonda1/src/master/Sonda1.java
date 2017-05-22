package master;
import java.io.*;
import java.rmi.RemoteException;
import java.lang.Exception;


public class Sonda1
{	
	String sonda = "sensor1.txt";
	File archivo = new File(System.getProperty("user.home"), "/Desktop/sensor1.txt"); 
	File users = new File(System.getProperty("user.home"), "/Desktop/users1.txt");
	boolean loged = false;
	
	public void login(String user, String pass){
		int cont = 0;
		
		try {
			
   			BufferedReader in = new BufferedReader(new FileReader(users));
        	String str;
       		 while ((str = in.readLine()) != null) {
            		cont++;
       		 }
	        in.close();
		} catch (IOException e) {
			System.out.println("error al leer users: " + users);
		}
		
		String[] cadenas = new String[cont];
		cont = 0;
		
		try {
   			BufferedReader in = new BufferedReader(new FileReader(users));
        	String str;
       		 while ((str = in.readLine()) != null) {
            		cadenas[cont] = str;
            		cont++;
       		 }
	        in.close();
		} catch (IOException e) {
			System.out.println("error al leer users: " + users);
		}
		
		for(int i = 0; i < cadenas.length; i++){
			
			String[] substrings = cadenas[i].split("#");
			
			if(substrings[0].equals(user) && substrings[1].equals(pass)){
				loged = true;
			}
		}
	}
	
	
	public String get(String peticion) throws RemoteException{
		
		String respuesta = "PETICION INCORRECTA";
		int cont = 0;

		String[] cadenas = new String[3];
		
	   	try {
	   			BufferedReader in = new BufferedReader(new FileReader(archivo));
	        	String str;
	       		 while ((str = in.readLine()) != null) {
	            		cadenas[cont] = str;
				cont++;
	       		 }
		        in.close();
		} catch (IOException e) {
			System.out.println("error al leer sensor: " + sonda);
		}
	   
		if(peticion.equals("volumen")){

			String[] respvol = cadenas[0].split("=");
			respuesta = respvol[1];

		}else if(peticion.equals("ultimafecha") || peticion.equals("fecha")) {

			String[] respultfech = cadenas[1].split("=");
			respuesta = respultfech[1];

		}else if(peticion.equals("luz") || peticion.equals("led")){
			
			String[] respled = cadenas[2].split("=");
			respuesta = respled[1];
		}
		return respuesta;
	}
	
	
	public void set(String peticion,String cambio) {
	
		int cont = 0;

		String[] cadenas = new String[3];

	   	try {
	        	BufferedReader in = new BufferedReader(new FileReader(archivo));
	        	String str;
	       		 while ((str = in.readLine()) != null) {
	            		cadenas[cont] = str;
				cont++;
	       		 }
		        in.close();
		} catch (IOException e) {
			System.out.println("error al leer sensor: " + sonda);
		}

		//el 0 sera el parametro y el 1 sera el valor del parametro
		String[] respvol = cadenas[0].split("=");
		String[] respultfech = cadenas[1].split("=");
		String[] respled = cadenas[2].split("=");

		if(peticion.equals("setvolumen")){

			respvol[1] = cambio;

		}else if(peticion.equals("setfecha")) {
			
			respultfech[1] = cambio;

		}else if(peticion.equals("setluz") || peticion.equals("setled")){
			
			respled[1] = cambio;
		}
		
		System.out.println("respvol: " + respvol);
		System.out.println("respultfech: " + respultfech);
		System.out.println("respled: " + respled);
	

		try {

           		BufferedWriter writer = new BufferedWriter(new FileWriter(archivo));
            		writer.write(respvol[0] + "=" + respvol[1] + '\n' +
						     respultfech[0] + "=" + respultfech[1] + '\n' +
						     respled[0]+ "=" + respled[1]);
			writer.close();

        } catch (Exception e) {
		
        	System.out.println("error al modificar el sensor: " + sonda);
    	}
	}
}
