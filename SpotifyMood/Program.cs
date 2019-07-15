using System;
using System.Collections.Generic;
using System.Threading;
using PerformMetrics;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using SpotifyEmotivInterface;
using SpotifyMood;



namespace SpotifyAPI.Web.Example
{
    public static class Program
    {
        private static string _clientId = "d959c53d82124f4a89b0b8f463152037"; //"";
        private static string _secretId = "3605d0cde2d74c07822299c0359f840f"; //"";
        

        // ReSharper disable once UnusedParameter.Local
        static public void Main()
        {

            //_clientId = string.IsNullOrEmpty(_clientId)
            //    ? Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID")
            //    : _clientId;

            //_secretId = string.IsNullOrEmpty(_secretId)
            //    ? Environment.GetEnvironmentVariable("SPOTIFY_SECRET_ID")
            //    : _secretId;


           //MainWindow mainMenu = new MainWindow();
            
            AuthorizationCodeAuth auth =
                new AuthorizationCodeAuth(_clientId, _secretId, "http://localhost:4002", "http://localhost:4002",
                    Scope.PlaylistReadPrivate | Scope.PlaylistReadCollaborative|Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);
            //Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
            //Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
            //Scope.UserRead+RecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState)




            Console.WriteLine("Hello, open Spotify please and start a Playback");

            Console.WriteLine("Put the Emotic Epoc+ on your head please and open Emotiv App");
            Console.WriteLine("Select your Emotiv epoc+ device and follow the instructions given by the application ");
            Console.WriteLine("you must be 100 % of connection for the application work properly  ");
            Console.WriteLine("Press Enter when you are ready");
            Console.ReadLine();

           


                auth.AuthReceived += AuthOnAuthReceived;

            auth.Start();



            auth.OpenBrowser();

            Thread.Sleep(20000);
            Console.ReadLine();


            auth.Stop(0);


        }

        private static async void AuthOnAuthReceived(object sender, AuthorizationCode payload)
        {
            AuthorizationCodeAuth auth = (AuthorizationCodeAuth) sender;
            auth.Stop();
            

            Token token = await auth.ExchangeCode(payload.Code);
           

            SpotifyWebAPI api = new SpotifyWebAPI
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType
               
        };
            ChooseMusicMood(api);
           
        }

        private static async void ChooseMusicMood(SpotifyWebAPI api)
        {




            
            PrivateProfile profile = await api.GetPrivateProfileAsync();
            
            string name = string.IsNullOrEmpty(profile.DisplayName) ? profile.Id : profile.DisplayName;
            
            

            Console.WriteLine($"Hello there, {name}!");

           

            Console.WriteLine("Your playlists:");
           
            Paging<SimplePlaylist> playlists = await api.GetUserPlaylistsAsync(profile.Id);
            //do
            //{
            playlists.Items.ForEach(playlist =>
            {
                 Console.WriteLine($"- {playlist.Name}");


            });

            PlaybackContext context = api.GetPlayback();
            if (context.IsPlaying)
            {
                Console.WriteLine($"You are listening {context.Item.Name}!");
            }

            Console.WriteLine("do you want to listen to music related to your mood ? (yes/no)");
            
           
            
            while(true)
            {
                string risposta = Console.ReadLine();

                if (risposta.Equals("Y") || risposta.Equals("yes") || risposta.Equals("y") || risposta.Equals("YES"))
                {
                    Console.WriteLine("Yes");
                    List<FullPlaylist> Playlists = new List<FullPlaylist>();
                    Console.WriteLine("choose your playlists to play according to your mood.");
                    Thread.Sleep(2000);
                    Console.WriteLine("type 1 to use default playlists ");
                    Console.WriteLine("type 2 to load your own playlists");
                    Console.WriteLine("type 3 to create your own playlists");


                    while (true)
                        
                    {
                        string mode = Console.ReadLine();

                        if (mode.Equals("1"))
                        {
                            Playlists = PlayMusic.GetPlaylists(api);
                            break;
                        }

                        else if(mode.Equals("2"))
                        {
                            Console.WriteLine("Please enter the Uri of yours playlists you want to load");
                            Playlists = PlayMusic.LoadPlaylists(api);
                            break;
                        }

                        else if (mode.Equals("3"))
                        {
                            Playlists = PlayMusic.CreatePLaylists(api);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Enter 1,2 or 3 please");
                        }
                    }
                


                    FullPlaylist PlaylistMood = PlayMusic.GetPlaylistMood(Playlists);
                    Console.WriteLine("Let's me choose a song for you:");
                    PlayMusic.ChangePlayback(api, PlaylistMood);
                    PlaybackContext context2 = api.GetPlayback();
          
                    Console.WriteLine($"You are listening {context2.Item.Name} now!");

                    PlayMusic.ManagePlayback(api);



                    Thread.Sleep(1000);

                    //Prog.Unsubscribe();

                    

                    break;
                }
                else if (risposta.Equals("N") || risposta.Equals("NO") || risposta.Equals("n") || risposta.Equals("no"))
                {
                    Console.WriteLine("No");
                    Console.WriteLine("Ok, enjoy your song ");
                    break;
                }

                else
                {
                    Console.WriteLine("Enter yes or no please");
                }

            }




            
            Console.WriteLine("Thanks for trying the App");
            

            




        }

        
    }
}