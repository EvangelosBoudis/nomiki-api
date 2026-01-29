ef-cli: 
	dotnet tool install dotnet-ef --version 8.0.2 --allow-downgrade --create-manifest-if-needed

run-app:
	docker-compose -f docker-compose.yml -p nomiki-app up

add-init-migrations:
	dotnet ef migrations add Init --project src/Nomiki.Api -o InterestRate/Database/Migrations -v