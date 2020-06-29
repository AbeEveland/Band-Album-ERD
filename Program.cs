using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Music
{
    class Band
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
        public List<Album> Albums { get; set; }
    }
    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int BandId { get; set; }
        public Band Band { get; set; }
    }
    class MusicContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.UseNpgsql("server=localhost;database=Music");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MusicContext();
            var bands = context.Bands.Include(band => band.Albums);

            while (1 == 1)
            {
                Console.WriteLine($"1 - View Bands");
                Console.WriteLine($"2 - Add an album or a band");
                Console.WriteLine($"3 - Release a band");
                Console.WriteLine($"4 - Sign a band");
                Console.WriteLine($"5 - View band's albums");
                Console.WriteLine($"6 - View all albums");
                Console.WriteLine($"7 - View all signed bands");
                Console.WriteLine($"8 - View all unsigned bands");
                Console.WriteLine($"Q - quit the application");
                Console.WriteLine();
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    var bandlist = context.Bands.Select(bands => bands.Name);
                    foreach (var band in bandlist)
                    {
                        Console.WriteLine(band);
                    }
                }

                if (choice == "2")
                {
                    Console.Clear();
                    Console.WriteLine($"Would you like to add a (B)and, or (Al)bum?");
                }

                if (choice == "b" || choice == "B")
                {
                    Console.Clear();
                    Console.WriteLine($"Please type name of band and press enter.");
                    var newBand = Console.ReadLine();

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

                    Band newbandaddition = new Band
                    {
                        Name = newBand,
                        CountryOfOrigin = newCountryOfOrigin,
                        NumberOfMembers = newNumberOfMembers,
                        Website = newWebsite,
                        Style = newStyle,
                        IsSigned = newIsSigned,
                        ContactName = newContactName,
                        ContactPhoneNumber = newContactPhoneNumber,
                    };
                    context.Add(newbandaddition);

                    context.SaveChanges();

                    Console.WriteLine($"Thank you! Your band has been added.");
                }

                if (choice == "al")
                {
                    Console.Clear();
                    Console.WriteLine($"Please type name of Album and press enter.");
                    var newAlbum = Console.ReadLine();

                    Console.Write("Is this album Explicit? ");
                    var newIsExplicit = Convert.ToBoolean(Console.ReadLine());

                    Console.Write("Please enter release date in this format: yyyy-mm-dd: ");
                    var newReleaseDate = DateTime.Parse(Console.ReadLine());

                    Console.Write("What is the band id??");
                    var newBandId = Int32.Parse(Console.ReadLine());

                    Album newAlbumaddition = new Album
                    {
                        Title = newAlbum,
                        IsExplicit = newIsExplicit,
                        ReleaseDate = newReleaseDate,
                        BandId = newBandId,

                    };
                    context.Albums.Add(newAlbumaddition);

                    context.SaveChanges();

                    Console.WriteLine($"Thank you! {newAlbum} has been added.");
                }

                if (choice == "3")
                {
                    Console.Clear();
                    Console.WriteLine($"Which band would you like to release?");
                    var bandToRelease = Console.ReadLine();
                    var ifbandExists = bands.Any(band => band.Name == bandToRelease);
                    var selectedBand = bands.FirstOrDefault(band => band.Name == bandToRelease);

                    if (ifbandExists)
                    {

                        selectedBand.IsSigned = false;
                        Console.WriteLine($"You let released {bandToRelease} ");
                        context.SaveChanges();
                    }

                    if (!ifbandExists)
                    {
                        Console.WriteLine($"{bandToRelease} does not exist. Press any ket to continue");

                    }
                }

                if (choice == "4")
                {
                    Console.Clear();
                    Console.WriteLine($"Which band would you like to sign?");
                    var bandToSign = Console.ReadLine();
                    var ifbandExists = bands.Any(band => band.Name == bandToSign);
                    var selectedBand = bands.FirstOrDefault(band => band.Name == bandToSign);

                    if (ifbandExists)
                    {

                        selectedBand.IsSigned = true;
                        Console.WriteLine($"You signed {bandToSign} ");
                        context.SaveChanges();
                    }

                    if (!ifbandExists)
                    {
                        Console.WriteLine($"{bandToSign} does not exist. Press any ket to continue");

                    }
                }

                if (choice == "5")
                {
                    Console.Clear();
                    Console.WriteLine($"Which band's album would you like to see?");
                    var Albums = Console.ReadLine();

                    var ifBandExists = bands.Any(band => band.Name == Albums);
                    var selectedBand = bands.FirstOrDefault(band => band.Name == Albums);

                    if (ifBandExists)
                    {
                        Console.WriteLine($"\nThere are {selectedBand.Albums.Count()} albums by {Albums}:\n");

                        foreach (var album in selectedBand.Albums)
                        {
                            Console.WriteLine($"* -- {album.Title}");
                        }
                    }
                }

                if (choice == "6")
                {
                    Console.Clear();
                    Console.WriteLine($"These are all of the albums in the database ordered by the release date:\n");

                    var albumsOrderedByReleaseDate = context.Albums.OrderBy(album => album.ReleaseDate);

                    foreach (var album in albumsOrderedByReleaseDate)
                    {
                        Console.WriteLine($"* -- {album.ReleaseDate.ToString("MM/dd/yyyy")} ----- \"{album.Title}\"");
                    }
                }

                if (choice == "7")
                {
                    Console.Clear();
                    {
                        Console.WriteLine($"These are all of the signed bands in the database: ");

                        var signedBands = bands.Where(band => band.IsSigned == true);

                        foreach (var band in signedBands)
                        {
                            Console.WriteLine($"* -- {band.Name}");
                        }
                    }

                    if (choice == "8")
                    {
                        Console.Clear();
                        {
                            Console.WriteLine($"These are all of the signed bands in the database: ");

                            var signedBands = bands.Where(band => band.IsSigned == false);

                            foreach (var band in signedBands)
                            {
                                Console.WriteLine($"* -- {band.Name}");
                            }

                        }
                        if (choice == "Q" || choice == "q") break;
                    }
                }
            }
        }
    }
}

