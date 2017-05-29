Prerequisite
- install nodejs 
- run command: `$ npm install -g gulp-cli`
- go to project directory: `$ cd ./src/AirDoc`
- run command: `$ npm install`
- Visual Studio 2017 with .Net core installed.

It's time to use Gulp tasks:
- `$ gulp` or `$ gulp build` to build an Production (optimized) version of your application
- `$ gulp dev` for Front-end developer

Run Back-end (Server side):
- `$ dotnet restore`
- `$ dotnet ef database update`
- `$ dotnet run`