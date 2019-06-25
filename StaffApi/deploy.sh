dotnet publish -c Release 

cp dockerfile ./bin/release/netcoreapp2.2/publish

docker build -t sdg-staff-directory-app-image ./bin/release/netcoreapp2.2/publish

docker tag sdg-staff-directory-app-image registry.heroku.com/sdg-staff-directory-app/web

docker push registry.heroku.com/sdg-staff-directory-app/web

heroku container:release web -a sdg-staff-directory-app

# sudo chmod 755 deploy.sh
# ./deploy.sh