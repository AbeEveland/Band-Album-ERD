using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections;

namespace Music
{
    class Bands
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public bool IsSigned { get; set; }
        public string ContactName { get; set; }
        public int ContactPhoneNumber { get; set; }
        public int AlbumId { get; set; }
        public Albums Album { get; set; }


    }
    class Albums
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
    class MusicContext : DbContext
    {
        public DbSet<Bands> Bands { get; set; }
        public DbSet<Albums> Album { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.UseNpgsql("server=localhost;database=Music");
        }
    }
    class Program
    {

        //private static string choice;

        static void Main(string[] args)
        {
            var context = new MusicContext();
            var bands = context.Bands.Include(band => band.Album);
            var listofbands = new List<Bands>();
            //var albums = context.Bands.Include(band => band.Album);


            //   var BandCount = Bands.Count();
            // Console.WriteLine($"There are {BandCount} !");

            // foreach (var Band in Bands)
            // {
            //     if (Band.Album == null)
            //     {
            //         Console.WriteLine($"There is a Band named {Band.Name} but not on this album");
            //     }
            //    else
            //    {
            //        Console.WriteLine($"There is a band named {Band.Name} and is on {Band.Album.Title}");
            //   }
            while (1 == 1)
            {
                Console.Write("(a)dd Band, (v)iew Band, (q)uit,");
                string choice = Console.ReadLine();


                if (choice == "v" || choice == "V")
                {
                    var bandlist = context.Bands.Select(bands => bands.Name);
                    foreach (var band in bandlist)
                    {
                        Console.WriteLine(band);
                    }



                    // foreach (var band in bands)
                    //{
                    // if (band.Album == null)
                    //{
                    // Console.WriteLine($"There is a band named {band.Name} but does not have an album.");

                    // }
                    // else
                    //  {
                    //     Console.WriteLine($"There is a band named {band.Name} and has an album named {band.Album.Title}");

                    //  }

                    //Console.WriteLine(Band.Name);

                    //  }
                    //foreach (var band in bands)
                    //{
                    //  if (band.Albums == null)
                    // {
                    //     Console.WriteLine($" These are your bands: {band.Name} but has no album");

                    //  }
                    // else
                    // {
                    //     Console.WriteLine($"There is a band named{band.Name} on a album named {bands.Albums.Title}");

                    // }

                }



                if (choice == "a" || choice == "A")
                {
                    Console.WriteLine($"Please type name of band and press enter.");
                    var newName = Console.ReadLine();

                    Console.Write("CountryOfOrigin: ");
                    var newCountryOfOrigin = Console.ReadLine();

                    Console.Write(" NumberOfMembers: ");
                    var newNumberOfMembers = Int32.Parse(Console.ReadLine());

                    Console.Write("Website: ");
                    var newWebsite = Console.ReadLine();

                    Console.Write("Style: ");
                    var newStyle = Console.ReadLine();

                    Console.Write("IsSigned: ");
                    bool newIsSigned = Convert.ToBoolean(Console.ReadLine());

                    Console.Write(" ContactName: ");
                    var newContactName = (Console.ReadLine());

                    Console.Write("ContactPhoneNumber: ");
                    var newContactPhoneNumber = Int32.Parse(Console.ReadLine());

                    Console.Write("AlbumId: ");
                    var newAlbumId = Int32.Parse(Console.ReadLine());


                    Bands newbandaddition = new Bands
                    {
                        Name = newName,
                        CountryOfOrigin = newCountryOfOrigin,
                        NumberOfMembers = newNumberOfMembers,
                        Website = newWebsite,
                        Style = newStyle,
                        IsSigned = newIsSigned,
                        ContactName = newContactName,
                        ContactPhoneNumber = newContactPhoneNumber,
                        AlbumId = newAlbumId,
                    };
                    context.Add(newbandaddition);

                    context.SaveChanges();

                    Console.WriteLine($"Thank you! Your band has been added.");



                }


                if (choice == "Q" || choice == "q") break;


            }
        }
    }
}


