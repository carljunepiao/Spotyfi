using System;
using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Domain.AlbumAgg;
using SpotyfiLib.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace SpotyfiLib.Infrastructure.UnitOfWork
{
    public class SecondGenUnitOfWork : DbContext, IUnitOfWork
    {
        public DbSet<Artist> Artists {get;set;}
        public DbSet<Track> Tracks {get;set;}
        public DbSet<Album> Albums {get;set;}
        public DbSet<Genre> Genres {get;set;}

        public SecondGenUnitOfWork(DbContextOptions<SecondGenUnitOfWork> options):base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Artist>(artist =>
            {
                artist.HasKey("Id");
                artist.Property("Id").HasColumnName("id");
                artist.HasOne(a => a.ArtistDetails).WithOne(a => a.Artist).HasForeignKey<ArtistDetail>(ad => ad.Id);
                artist.Property("FirstName").HasColumnName("firstname");
                artist.Property("LastName").HasColumnName("lastname");
                artist.Property("YearDebut").HasColumnName("year_debut");
                artist.Property(p => p.ConcurrencyStamp).ForNpgsqlHasColumnName("xmin").ForNpgsqlHasColumnType("xid").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
                artist.ToTable("artist");
            });

            builder.Entity<ArtistDetail>(artistDetail =>
            {
                artistDetail.HasKey("Id");
                artistDetail.Property("Id").HasColumnName("artist_id");
                artistDetail.Property("StageName").HasColumnName("stage_name");
                artistDetail.ToTable("artist_detail");
            });

            builder.Entity<Track>(track =>
            {
                track.HasKey("Id");
                track.Property("Id").HasColumnName("id");
                track.Property("TrackTitle").HasColumnName("track_title");
                track.Property("Duration").HasColumnName("duration");
                track.Property("ReleaseDate").HasColumnName("release_date");
                track.Property(p => p.ConcurrencyStamp).ForNpgsqlHasColumnName("xmin").ForNpgsqlHasColumnType("xid").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
                track.ToTable("track");
            });

            builder.Entity<Album>(album =>
            {
                album.HasKey("Id");
                album.Property("Id").HasColumnName("id");
                album.Property("AlbumTitle").HasColumnName("album_title");
                album.Property("RecordLabel").HasColumnName("record_label");
                album.Property("Year").HasColumnName("release_date");
                album.Property(p => p.ConcurrencyStamp).ForNpgsqlHasColumnName("xmin").ForNpgsqlHasColumnType("xid").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
                album.ToTable("album");
            });

            builder.Entity<Genre>(genre =>
            {
                genre.HasKey("Id");
                genre.Property("Id").HasColumnName("id");
                genre.Property("Type").HasColumnName("type");
                genre.Property(p => p.ConcurrencyStamp).ForNpgsqlHasColumnName("xmin").ForNpgsqlHasColumnType("xid").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();                
                genre.ToTable("genre");
            });

            builder.Entity<ArtistAlbum>(artist_album =>
            {
                artist_album.HasKey(ad => new { ad.ArtistId, ad.AlbumId });
                artist_album.Property("ArtistId").HasColumnName("artist_id");
                artist_album.Property("AlbumId").HasColumnName("album_id");
                artist_album.HasOne(ab => ab.Artist).WithMany(a => a.Albums).HasForeignKey(ab => ab.ArtistId);
                artist_album.HasOne(ab => ab.Album).WithMany(a => a.Artists).HasForeignKey(ab => ab.AlbumId);
                //artist_album.HasOne(ab => ab.Artist).WithMany(a => a.Albums);
                //artist_album.HasOne(ab => ab.Album).WithMany(a => a.Artists);
                artist_album.ToTable("artist_album");
            });

            builder.Entity<ArtistTrack>(artist_track =>
            {
                artist_track.HasKey(ad => new { ad.ArtistId, ad.TrackId });
                artist_track.Property("ArtistId").HasColumnName("artist_id");
                artist_track.Property("TrackId").HasColumnName("track_id");
                artist_track.HasOne(ab => ab.Artist).WithMany(a => a.Tracks).HasForeignKey(ab => ab.ArtistId);
                artist_track.HasOne(ab => ab.Track).WithMany(a => a.Artists).HasForeignKey(ab => ab.TrackId);
                //artist_track.HasOne(ab => ab.Artist).WithMany(a => a.Tracks);
                //artist_track.HasOne(ab => ab.Track).WithMany(a => a.Artists);
                artist_track.ToTable("artist_track");
            });

            builder.Entity<TrackAlbum>(track_album =>
            {
                track_album.HasKey(ad => new { ad.TrackId, ad.AlbumId });
                track_album.Property("TrackId").HasColumnName("track_id");
                track_album.Property("AlbumId").HasColumnName("album_id");
                track_album.HasOne(ab => ab.Track).WithMany(a => a.Albums).HasForeignKey(ab => ab.TrackId);
                track_album.HasOne(ab => ab.Album).WithMany(a => a.Tracks).HasForeignKey(ab => ab.AlbumId);
                //track_album.HasOne(ab => ab.Track).WithMany(a => a.Albums);
                //track_album.HasOne(ab => ab.Album).WithMany(a => a.Tracks);
                track_album.ToTable("track_album");
            });

            builder.Entity<TrackGenre>(track_genre =>
            {
                track_genre.HasKey(ad => new { ad.TrackId, ad.GenreId });
                track_genre.Property("TrackId").HasColumnName("track_id");
                track_genre.Property("GenreId").HasColumnName("genre_id");
                track_genre.HasOne(ab => ab.Track).WithMany(a => a.Genres).HasForeignKey(ab => ab.TrackId);
                track_genre.HasOne(ab => ab.Genre).WithMany(a => a.Tracks).HasForeignKey(ab => ab.GenreId);
                track_genre.ToTable("track_genre");
            });

            base.OnModelCreating(builder);
        }

        public void Commit(){ SaveChanges(); }
        public void Rollback(){ Dispose(); }

    }
}