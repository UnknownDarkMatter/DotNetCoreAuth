
##########################################################################################
#				DockerFile
##########################################################################################

ouvrir cmd dans repertoire du DockerFile

docker build -t identityserver4-demo .
docker run --rm -p 5000:5000 identityserver4-demo
	--> le -p 5000:5000 permet de passer tout ce qui vient de l'interface du host vers l'interface du container
			DOC :
				Connection refused? Docker networking and how it impacts your image
					https://pythonspeed.com/articles/docker-connection-refused/
					
					
####### utils
docker images
docker ps -a
docker rmi identityserver4-demo  (rm image)
docker container ls -a
docker stop 727e135cd6ee  
docker port 992dbc87dcf8

docker start 6d8c95d02c7b  
docker container ls -a
docker rm 093e9f4c7ef3     (rm container)
docker exec 5817cf8add95 ls /home/IdentityServer
docker  network ls
docker network inspect d74a5ab6c485
docker inspect 17886bc1a648 (container_name_or_id)

##########################################################################################
#				DockerCompose
##########################################################################################

docker-compose config
docker-compose up	
docker-compose ps

docker-compose stop
docker-compose down

Unable to configure HTTPS endpoint. No server certificate was specified, and the default developer certificate could not be found or is out of date.


##########################################################################################
#				Gestion container
##########################################################################################
savoir la version de linux (Debian GNU/Linux 10 (buster))
docker exec 5817cf8add95 cat /etc/os-release
docker exec 5817cf8add95 apt install curl
C:\sources\DotNetCoreAuth\Samples.IdentityServer4\Docker>docker exec 5817cf8add95 cat /etc/apt/sources.list
	# deb http://snapshot.debian.org/archive/debian/20210927T000000Z buster main
	deb http://deb.debian.org/debian buster main
	# deb http://snapshot.debian.org/archive/debian-security/20210927T000000Z buster/updates main
	deb http://security.debian.org/debian-security buster/updates main
	# deb http://snapshot.debian.org/archive/debian/20210927T000000Z buster-updates main
	deb http://deb.debian.org/debian buster-updates main

	
######### DOC
WSL2		
	https://docs.docker.com/desktop/windows/wsl/#develop-with-docker-and-wsl-2
	
	
	
	