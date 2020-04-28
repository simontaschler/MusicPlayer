using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Models;
using Refit;

namespace MusicPlayer.Services
{
    interface IMusicPlayerAPI
    {
        [Get("/index.php/Artist/listAll")]
        Task<List<Artist>> GetAllArtists();

        [Get("/index.php/Artist/get/{artistID}")]
        Task<Artist> GetArtist(int artistID);

        [Get("/index.php/Artist/getAlbums/{artistID}")]
        Task<List<Album>> GetArtistAlbums(int artistID);

        [Get("/index.php/Artist/getSongs/{artistID}")]
        Task<List<Song>> GetArtistSongs(int artistID);

        [Get("/index.php/Artist/getAlbumCovers/{artistID}")]
        Task<List<Album>> GetAlbumCovers(int artistID);

        [Get("/index.php/Album/listAll")]
        Task<List<Album>> GetAllAlbums();

        [Get("/index.php/Album/get/{albumID}")]
        Task<Album> GetAlbum(int albumID);

        [Get("/index.php/Album/getArtists/{albumID}")]
        Task<List<Artist>> GetAlbumArtists(int albumID);

        [Get("/index.php/Album/getSongs/{albumID}")]
        Task<List<Song>> GetAlbumSongs(int albumID);

        [Get("/index.php/Album/getCover/{albumID}")]
        Task<string> GetAlbumCover(int albumID);

        [Get("/index.php/Song/getArtists/{songID}")]
        Task<List<Artist>> GetSongArtists(int songID);
        
        [Get("/index.php/Song/getArtistNames/{songID}")]
        Task<List<string>> GetSongArtistNames(int songID);        
    }
}
