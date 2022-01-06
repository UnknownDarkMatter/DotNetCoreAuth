####### Comment installer le certificat SSL pour avoir un certificat SSL valide dans Chrome ##############

télécharger OpenSSL : https://sourceforge.net/projects/openssl/
copier le contenu du bin dans C:\OpenSSL

executer make-cert.bat

double cliquer sur localhost.pfx afin d'installer le certificat
	le password est dans le fichier make-cert.bat
	quand il est demandé de choisir le magasin (le store en anglais),
		"Place all certificate in the following store"
			"Trust root certificate authorities"

dans la commande executee du package json on indique le fichier de key et le certificat
	    "start-pwa": "ng build --prod && angular-http-server -p 4200 --https --path dist/my-angular-boilerplate --key ./ssl/localhost.key --cert ./ssl/localhost.crt"

il est possible de retirer le certificat du magasin avec la commande certmgr.msc
	voir article "How to: View certificates with the MMC snap-in"
		https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/how-to-view-certificates-with-the-mmc-snap-in

