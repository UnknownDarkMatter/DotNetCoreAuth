## sur debian
cp localhost.crt /usr/share/ca-certificates
echo "localhost.crt" >> /etc/ca-certificates.conf
update-ca-certificates
