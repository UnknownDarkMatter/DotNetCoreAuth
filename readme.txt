
###########################################################################################
#  Mise en place du projet Samples.IdentityServer4
###########################################################################################

il existe deux configurations : 
	- application en localhost hebergée via Visual Studio sans utiliser Docker
	- application hébergée dans des containers Docker

Suivant la config il faut générer des certificats et des clés différentes car les noms de monaines ne sont pas les mêmes
A la fin de ce fichier, de la doc diverse et des manips de gestion des certificats SSL qui ne fonctionnent qu'en localhost (incompatible avec containairisation Docker)

Dans la solution Visual Studio on n'utilise que les projets suivants
	- ProCodeGuide.IdServer4.Client
	- ProCodeGuide.Samples.IdentityServer4

###########################################################################################
#  application en localhost hebergée via Visual Studio sans utiliser Docker
###########################################################################################

##### configurer OpenSSL
télécharger OpenSSL : https://sourceforge.net/projects/openssl/
copier le contenu du bin dans C:\OpenSSL
Ajouter C:\OpenSSL a la variable d'environnement path de Windows

Créer un repertoire de travail C:\sources\DotNetCoreAuth\Samples.IdentityServer4\OpenSSL
creer un fichier localhost.docker.conf contenant dans les alt_names les noms de domaines à autoriser
	[req]
	default_bits = 2048
	prompt = no
	default_md = sha256
	x509_extensions = v3_req
	req_extensions = v3_req
	distinguished_name = dn

	[ dn ]
	C=FR
	ST=Avenue Andre Morizet
	L=Boulogne Billancourt
	O=End Point
	OU=Docker and localhost
	emailAddress=toto@toto.com
	CN = localhost


	[ v3_req ]
	# Extensions to add to a certificate request
	subjectAltName = @alt_names

	[ alt_names ]
	DNS.1 = localhost
	DNS.2 = myidentityserver
	DNS.3 = mywebsite

######## créer les certificats et clefs en localhost
ouvrir un prompt DOS dans le répertoire de travail C:\sources\DotNetCoreAuth\Samples.IdentityServer4\OpenSSL

openssl req -new -x509 -sha256 -nodes -newkey rsa:2048 -config localhost.docker.conf -out localhost.docker.crt -keyout localhost.docker.key

openssl pkcs12 -export -inkey localhost.docker.key -in localhost.docker.crt -out ..\ProCodeGuide.Samples.IdentityServer4\token-jwt-secret.pfx -passin pass:token-jwt-secret -passout pass:token-jwt-secret
openssl pkcs12 -export -inkey localhost.docker.key -in localhost.docker.crt -out ..\ProCodeGuide.Samples.IdentityServer4\token-jwt-secret-http2.pfx -passin pass:token-jwt-secret-http2 -passout pass:token-jwt-secret-http2
openssl pkcs12 -export -inkey localhost.docker.key -in localhost.docker.crt -out ..\ProCodeGuide.IdServer4.Client\token-jwt-secret-http2.pfx -passin pass:token-jwt-secret-http2 -passout pass:token-jwt-secret-http2

sous Windows il faut installer le certificat en double cliquant sur le crt (localhost.docker.crt), cliquer sur "installer un certificat" 
	puis choisir de l'installer sur l'ordinateur local puis choisir de l'installer dans le magasin "Autorités de certification racines de confiance"


######## ouvrir l'application
https://localhost:5003
lancer soit la solution en debug dans Visual Studio
soit lancer run-identityServer.bat et run-webSite.bat dans C:\sources\DotNetCoreAuth\Samples.IdentityServer4\Docker\bin

##### supprimer le certificat installé
chercher l'utilitaire "Gerer des certificats d'ordinateur" en tappant "cert" dans les programmes
supprimer les certificats crés avec powershell (le nom est égal au "common name", normalement c'est "localhost" avec les commandes ci-dessus)
!! ATTENTION !! il ne faut pas supprimer n'importe quel certificat autrement l'ordinateur risque de ne plus bien fonctionner
avant de supprimer un certificat il est conseillé de l'ouvrir et vérifier ses propriétés (ST = Avenue Andre Morizet, ...)



###########################################################################################
#  application hébergée dans des containers Docker
###########################################################################################


##### configurer OpenSSL
télécharger OpenSSL : https://sourceforge.net/projects/openssl/
copier le contenu du bin dans C:\OpenSSL
Ajouter C:\OpenSSL au path

Créer un repertoire de travail C:\sources\DotNetCoreAuth\Samples.IdentityServer4\OpenSSL
creer un fichier myidentityserver.docker.conf contenant dans les alt_names les noms de domaines à autoriser
	[req]
	default_bits = 2048
	prompt = no
	default_md = sha256
	x509_extensions = v3_req
	req_extensions = v3_req
	distinguished_name = dn

	[ dn ]
	C=FR
	ST=Avenue Andre Morizet
	L=Boulogne Billancourt
	O=End Point
	OU=Docker and localhost
	emailAddress=toto@toto.com
	CN = myidentityserver
	#CN = localhost


	[ v3_req ]
	# Extensions to add to a certificate request
	subjectAltName = @alt_names

	[ alt_names ]
	DNS.1 = localhost
	DNS.2 = myidentityserver
	DNS.3 = mywebsite

creer un fichier mywebsite.docker.crt contenant dans les alt_names les noms de domaines à autoriser
	[req]
	default_bits = 2048
	prompt = no
	default_md = sha256
	x509_extensions = v3_req
	req_extensions = v3_req
	distinguished_name = dn

	[ dn ]
	C=FR
	ST=Avenue Andre Morizet
	L=Boulogne Billancourt
	O=End Point
	OU=Docker and localhost
	emailAddress=toto@toto.com
	CN = mywebsite
	#CN = localhost


	[ v3_req ]
	# Extensions to add to a certificate request
	subjectAltName = @alt_names

	[ alt_names ]
	DNS.1 = localhost
	DNS.2 = myidentityserver
	DNS.3 = mywebsite


##### créer les certificats et clefs de docker
ouvrir un prompt DOS dans le répertoire C:\sources\DotNetCoreAuth\Samples.IdentityServer4\OpenSSL

openssl req -new -x509 -sha256 -nodes -newkey rsa:2048 -config myidentityserver.docker.conf -out myidentityserver.docker.crt -keyout myidentityserver.docker.key
openssl pkcs12 -export -inkey myidentityserver.docker.key -in myidentityserver.docker.crt -out ..\ProCodeGuide.Samples.IdentityServer4\token-jwt-secret.pfx -passin pass:token-jwt-secret -passout pass:token-jwt-secret
openssl pkcs12 -export -inkey myidentityserver.docker.key -in myidentityserver.docker.crt -out ..\ProCodeGuide.Samples.IdentityServer4\token-jwt-secret-http2.pfx -passin pass:token-jwt-secret-http2 -passout pass:token-jwt-secret-http2
xcopy /Y myidentityserver.docker.crt ..\ProCodeGuide.Samples.IdentityServer4\
xcopy /Y myidentityserver.docker.crt ..\ProCodeGuide.IdServer4.Client\


openssl req -new -x509 -sha256 -nodes -newkey rsa:2048 -config mywebsite.docker.conf -out mywebsite.docker.crt -keyout mywebsite.docker.key
openssl pkcs12 -export -inkey mywebsite.docker.key -in mywebsite.docker.crt -out ..\ProCodeGuide.IdServer4.Client\token-jwt-secret-http2.pfx -passin pass:token-jwt-secret-http2 -passout pass:token-jwt-secret-http2
xcopy /Y mywebsite.docker.crt ..\ProCodeGuide.Samples.IdentityServer4\
xcopy /Y mywebsite.docker.crt ..\ProCodeGuide.IdServer4.Client\

pour info, cette manip est faite par le docker-compose, sous linux il faut copier le crt dans /usr/share/ca-certificates, ajouter le nom du certificat dans /etc/ca-certificates.conf et lancer la commande update-ca-certificates

##### publier la solution pour mettre à jour C:\sources\DotNetCoreAuth\Samples.IdentityServer4\Docker\bin
	- projet ProCodeGuide.IdServer4.Client
	- projet ProCodeGuide.Samples.IdentityServer4


##### créer les certificats et clefs de docker
sur l'ordinateur hôte Windows, ajouter les lignes suivantes dans C:\Windows\System32\drivers\etc\hosts

#Docker containers
127.0.0.1 myidentityserver

######## ouvrir l'application
https://localhost:5003


###########################################################################################
#  documentation diverse
###########################################################################################

##### A propos de Identity Server

JWTAuth-master.zip
	JWT Authentication In ASP.NET Core
		https://www.freecodespot.com/blog/jwt-authentication-in-dotnet-core/


JwtAuthentication-master.zip
	JWT Authentication with Symmetric Encryption in ASP.NET Core
		https://eduardstefanescu.dev/2020/04/11/jwt-authentication-with-symmetric-encryption-in-asp-dotnet-core/
		https://github.com/StefanescuEduard/JwtAuthentication

Samples.IdentityServer4.zip
	Implement OAuth2 and OpenID Connect (Using IdentityServer4) in ASP.NET Core 5
		https://www.youtube.com/watch?v=vJqR_1dQIkc
		https://procodeguide.com/programming/oauth2-and-openid-connect-in-aspnet-core/
		https://github.com/procodeguide/ProCodeGuide.Samples.IdentityServer4

Connection refused? Docker networking and how it impacts your image
	https://pythonspeed.com/articles/docker-connection-refused/

##### création de certificat autosigné avec IIS puis export en pfx
l'export de certificat créé depuis inetmgr provoque une erreur NS_ERROR_NET_INADEQUATE_SECURITY

##### générer un certificat SSL en localhost avec "dotnet dev-certs"
##### les certificats doivent être approuvés pour être utilisés avec SSL
il faut créer un certificat racine de confiance avec "dotnet dev-certs https --trust" puis générer des certificats sous le certificat racine de confiance de dev

dotnet dev-certs https --clean
dotnet dev-certs https --trust
dotnet dev-certs https -p token-jwt-secret       -ep C:\sources\DotNetCoreAuth\Samples.IdentityServer4\ProCodeGuide.Samples.IdentityServer4\token-jwt-secret.pfx
dotnet dev-certs https -p token-jwt-secret-http2 -ep C:\sources\DotNetCoreAuth\Samples.IdentityServer4\ProCodeGuide.Samples.IdentityServer4\token-jwt-secret-http2.pfx
dotnet dev-certs https -p token-jwt-secret-http2 -ep C:\sources\DotNetCoreAuth\Samples.IdentityServer4\ProCodeGuide.IdServer4.Client\token-jwt-secret-http2.pfx


##### générer un certificat SSL en localhost avec powershell (New-SelfSignedCertificate)
#### Powershell en mode admin :
au lieu de creer le certificat avec "dotnet dev-certs https" on le crée via powershell en précisant des noms d'host qui sont les hostname des containers docker

	#create a SAN cert for both host.docker.internal and localhost
	#$cert = New-SelfSignedCertificate -DnsName "myidentityserver", "mywebsite", "localhost" -CertStoreLocation cert:\localmachine\my
	$cert = New-SelfSignedCertificate -DnsName @("myidentityserver", "mywebsite", "localhost") -CertStoreLocation cert:\localmachine\my
	#$cert = New-SelfSignedCertificate -DnsName @("server.docker.local", "localhost") -CertStoreLocation cert:\localmachine\my
   
	#export it for docker container to pick up later
	$password = ConvertTo-SecureString -String "token-jwt-secret" -Force -AsPlainText
	$certKeyPath = "C:\sources\DotNetCoreAuth\Samples.IdentityServer4\ProCodeGuide.Samples.IdentityServer4\token-jwt-secret.pfx"
	Export-PfxCertificate -Cert $cert -FilePath $certKeyPath -Password $password
	$rootCert1 = $(Import-PfxCertificate -FilePath $certKeyPath -CertStoreLocation 'Cert:\LocalMachine\Root' -Password $password)
	
	$password = ConvertTo-SecureString -String "token-jwt-secret-http2" -Force -AsPlainText
	$certKeyPath = "C:\sources\DotNetCoreAuth\Samples.IdentityServer4\ProCodeGuide.Samples.IdentityServer4\token-jwt-secret-http2.pfx"
	Export-PfxCertificate -Cert $cert -FilePath $certKeyPath -Password $password
	$rootCert2 = $(Import-PfxCertificate -FilePath $certKeyPath -CertStoreLocation 'Cert:\LocalMachine\Root' -Password $password)
	
	$password = ConvertTo-SecureString -String "token-jwt-secret-http2" -Force -AsPlainText
	$certKeyPath = "C:\sources\DotNetCoreAuth\Samples.IdentityServer4\ProCodeGuide.IdServer4.Client\token-jwt-secret-http2.pfx"
	Export-PfxCertificate -Cert $cert -FilePath $certKeyPath -Password $password
	$rootCert3 = $(Import-PfxCertificate -FilePath $certKeyPath -CertStoreLocation 'Cert:\LocalMachine\Root' -Password $password)

	# trust it on your host machine
	$store = New-Object System.Security.Cryptography.X509Certificates.X509Store "TrustedPublisher","LocalMachine"
	$store.Open("ReadWrite")
	$store.Add($cert)
	$store.Add($rootCert1)
	$store.Add($rootCert2)
	$store.Add($rootCert3)
	$store.Close()
	

##### supprimer des certificats générés avec powershell
chercher l'utilitaire "Gerer des certificats d'ordinateur" en tappant "cert" dans les programmes
supprimer les certificats crés avec powershell (le nom est égal au "common name", normalement c'est "myidentityserver" avec les commandes ci-dessus)
!! ATTENTION !! il ne faut pas supprimer n'importe quel certificat autrement l'ordinateur risque de ne plus bien fonctionner















