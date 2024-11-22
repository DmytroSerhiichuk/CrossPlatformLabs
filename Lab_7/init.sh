sudo apt-get update -y && sudo apt-get upgrade -y
sudo apt-get install -y wget apt-transport-https software-properties-common

timedatectl set-timezone Asia/Bangkok

# PostgreSql
sudo apt-get install -y postgresql postgresql-contrib
sudo -u postgres psql -c "ALTER USER postgres PASSWORD 'qwerty';"
systemctl restart postgresql
echo "PostgreSQL Installed."

# SQL Server
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2019.list)"

sudo apt-get update -y
sudo apt-get install -y mssql-server

sudo /opt/mssql/bin/mssql-conf setup accept-eula

sudo apt-get install -y mssql-tools unixodbc-dev

echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc

sudo /opt/mssql/bin/mssql-conf setup
sudo ACCEPT_EULA=Y /opt/mssql/bin/mssql-conf setup --accept-eula --sa-password "QAZwsxedc_1234" --sql-server-version "2019"

sudo systemctl enable mssql-server
sudo systemctl start mssql-server

echo "SQL Server Installed."

# SQLite
echo "Installing SQLite..."
sudo apt-get install -y sqlite3 libsqlite3-dev
echo "SQLite installed successfully."

# Node
curl -fsSL https://deb.nodesource.com/setup_20.x | sudo -E bash -
sudo apt-get install -y nodejs

# .NET
sudo apt-get update
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install -y dotnet-sdk-6.0

sudo dotnet tool install --global dotnet-ef --version 6.0.36

echo 'export PATH=$PATH:$HOME/.dotnet/tools' >> ~/.bashrc
export PATH=$PATH:$HOME/.dotnet/tools

rm -rf /home/vagrant/Lab_6
rm -rf /home/vagrant/Lab_7

mkdir -p /home/vagrant/Lab_6
mkdir -p /home/vagrant/Lab_7

cp -r /vagrant/Lab_6/* /home/vagrant/Lab_6/
cp -r /vagrant/Lab_7/* /home/vagrant/Lab_7/

dotnet build --project /home/vagrant/Lab_6/Lab_6.csproj
echo "Build Completed"