using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaMa.Via.Auctus.Infrastructure.Migrations
{
    /// <inheritdoc />
     public partial class InitialCarManagementSeedData : Migration 
    {
        //cars 
        private static readonly Guid _teslaY_Id = Guid.Parse("da25b825-fcbe-40a5-bd66-ac18b1956124");
        private static readonly Guid _teslaYUnregistered_Id = Guid.Parse("ac5c9cc5-e404-4da6-94e4-ad5f228303e6");
        
        //engines
        private static readonly Guid _teslaModelYLongRangeEngine_Id = Guid.Parse("a12b2222-6016-47dc-be8b-0b457ef45b41");
        private static readonly Guid _teslaModelYPerformanceEngine_Id = Guid.Parse("6f555de3-79e4-43fb-b984-1dc11382067b"); 
        
        //versions
        private static readonly Guid _teslaModelYLongRangeVersion_Id = Guid.Parse("7bcd787a-43b6-4f3f-803f-66f8ee9b52f3");
        private static readonly Guid _teslaModelYPerformanceVersion_Id = Guid.Parse("15ce5658-6c85-4cad-9fba-f9827681c8fa"); 
        
        // models
        private static readonly Guid _teslaModelY_Id = Guid.Parse("6baa0b32-fd6d-473b-80f2-07d999ed3619");
        private static readonly Guid _teslaModel3_Id = Guid.Parse("91a97967-44fb-4e60-a85a-8336493cabc6");
        private static readonly Guid _volkswagenID4_Id = Guid.Parse("00618ad8-1369-452c-8c50-32a4fcf897f3"); 
        
        // brands
        private static readonly Guid _teslaBrand_Id = Guid.Parse("f5c7c4b4-4faf-44cb-b8b9-41dcf58381d0");
        private static readonly Guid _bmwBrand_Id = Guid.Parse("50c1d215-2c29-404a-9b3d-0afea8ab1e35");
        private static readonly Guid _audiBrand_Id = Guid.Parse("5bdae480-7d05-47f4-b190-b96be93dc1db");
        private static readonly Guid _volkswagenBrand_Id = Guid.Parse("7a536b1a-d60f-4911-885d-59c154afdeea");
        private static readonly Guid _skodaBrand_Id = Guid.Parse("1f6c04b4-0e74-4f5b-989e-5819b9a123ee");

        
      protected override void Up(MigrationBuilder migrationBuilder)
        { 
            var env= System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); 
            if(env =="Development") 
            {
               SeedCarBrands(migrationBuilder);
            }
            SeedCarModels(migrationBuilder); 
            SeedCarModelVersions(migrationBuilder);
            SeedEngines(migrationBuilder);
            SeedVersionEngines(migrationBuilder);
            SeedCar(migrationBuilder);
        }

        private void SeedCar(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Cars",
                new[] { "Id", "BrandId","ModelId","VersionId", "EngineId", "Registration_LicensePlate","Registration_FirstRegistrationDate","Registration_RegistrationExpiryDate" },
                values: new object[,]
                {
                    {
                        _teslaY_Id,
                        _teslaBrand_Id,
                        _teslaModelY_Id,
                        _teslaModelYLongRangeVersion_Id,
                        _teslaModelYPerformanceEngine_Id,
                        "2tsl023",
                        DateOnly.Parse("2024-11-01"),
                        DateOnly.Parse("2024-11-01"),
                    },   
                    {
                        _teslaYUnregistered_Id,
                        _teslaBrand_Id,
                        _teslaModelY_Id,
                        _teslaModelYLongRangeVersion_Id,
                        _teslaModelYPerformanceEngine_Id,
                       null,
                       null,
                       null,
                    },   
                },
                columnTypes: new[] 
                { 
                    "uuid", 
                    "uuid",  
                    "uuid",  
                    "uuid",  
                    "uuid", 
                    "character varying(30)",
                    "date",
                    "date"
                });  
        }
        private void SeedVersionEngines(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("ModelVersionEngines",
                new[] { "EngineId", "ModelVersionId" },
                values: new object[,]
                {
                    {
                        _teslaModelYLongRangeEngine_Id,
                        _teslaModelYLongRangeVersion_Id 
                    },   
                    {
                        _teslaModelYPerformanceEngine_Id,
                       _teslaModelYPerformanceVersion_Id
                    },   
                },
                columnTypes: new[] 
                { 
                    "uuid", 
                    "uuid",  
                }); 
        }

        private void SeedEngines(MigrationBuilder migrationBuilder)
        {  
            migrationBuilder.InsertData("Engines",
                new[] { "Id", "Name", "FuelTypeId","HorsePower", "Torque","EngineEfficiencyValue","EngineEfficiencyUnit" },
                values: new object[,]
                {
                    {
                        _teslaModelYLongRangeEngine_Id,
                        "Tesla Model Y Long Range Engine",
                        "Electric",
                        331,
                        420,
                        15m,
                        "wh/km"
                    },   
                    {
                        _teslaModelYPerformanceEngine_Id,
                        "Tesla Model Y Performance Engine",
                        "Electric",
                        384,
                        487,
                        14.5m,
                        "wh/km"
                    },   
                },
                columnTypes: new[] 
                { 
                    "uuid", 
                    "varchar(255)",  
                    "varchar(255)",  
                    "integer", 
                    "integer",
                    "numeric", 
                    "varchar(20)", 
                }); 
        }

        private void SeedCarModelVersions(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("CarModelVersions",
                new[] { "Id", "CarModelId", "Name","Year", "ImageUrl" },
                values: new object[,]
                {
                    {
                        _teslaModelYLongRangeVersion_Id,
                        _teslaModelY_Id,
                        "Long Range All Wheel Drive",
                        2024,
                        null
                    },  
                    {
                       _teslaModelYPerformanceVersion_Id,
                        _teslaModelY_Id,
                        "Performance All Wheel Drive",
                        2024,
                        null
                    } 
                },
                columnTypes: new[] 
                { 
                    "uuid", 
                    "uuid",  
                    "varchar(255)", 
                    "integer",
                    "text" 
                });
        }

        private void SeedCarModels(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.InsertData("CarModels",
                new[] { "Id", "Name", "CarBrandId","ImageUrl" },
                values: new object[,]
                {
                    {
                        _teslaModelY_Id,
                        "Model Y",
                         _teslaBrand_Id,
                         null
                    },  
                    {
                        _teslaModel3_Id,
                        "Model 3",
                        _teslaBrand_Id,
                        null
                    }, {
                        _volkswagenID4_Id,
                        "ID 4",
                        _volkswagenBrand_Id,
                        "https://www.signline.de/out/cf_detail_images/a1afca4cc369685915f31f887dfeeb11_cf_ci_1.png"
                    }, 
                },
                columnTypes: new[] 
                { 
                    "uuid", 
                    "varchar(255)", 
                    "uuid",  
                    "text" 
                });
        }

        private static void SeedCarBrands(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("CarBrands",
                new[] { "Id", "Name", "Theme_PrimaryColor", "Theme_SecondaryColor", "Theme_FontFamily","Theme_LogoUrl" },
                values: new object[,]
                {
                    {
                        _teslaBrand_Id,
                        "Tesla",
                        "#CC0000",
                        "#333333",
                        "Roboto, sans-serif",
                        "tesla.svg"
                    },
                    {
                        _bmwBrand_Id,  
                        "BMW",
                        "#0066B1",
                        "#FFFFFF",
                        "Helvetica, Arial, sans-serif",
                        "bmw.svg"
                    },
                    {
                        _audiBrand_Id, 
                        "Audi",
                        "#BB0A30",
                        "#000000",
                        "AudiType, sans-serif",
                        "https://upload.wikimedia.org/wikipedia/commons/9/92/Audi-Logo_2016.svg"
                    },
                    {
                        _volkswagenBrand_Id,  
                        "Volkswagen",
                        "#001489",
                        "#FFFFFF",
                        "VolkswagenAG, sans-serif",
                        "https://upload.wikimedia.org/wikipedia/commons/6/6d/Volkswagen_logo_2019.svg"
                    },
                    {
                        _skodaBrand_Id,  
                        "Skoda",
                        "#007C30",
                        "#FFFFFF",
                        "SkodaPro, sans-serif",
                        "https://cdn.skoda-auto.com/resources/Modules_c2911565-9b23-426f-b6df-3f86a0935d42/Assets/img/SkodaLogoNew.svg"
                    }
                },
                columnTypes: new[] 
                { 
                    "uuid", 
                    "varchar(255)", 
                    "varchar(30)", 
                    "varchar(30)", 
                    "text", 
                    "text" 
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
