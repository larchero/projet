using System;
using System.Collections.Generic;
using System.Text;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using Newtonsoft.Json;
using SpotifyAPI.Web.Auth;
using System.Net;
using System.IO;
using PerformMetrics;
using CortexAccess;
using System.Collections;
using System.Threading;

using System.Diagnostics;

using System.Windows.Input;

namespace SpotifyMood
{
    class PlayMusic
    {
        


        public static List<FullPlaylist> GetPlaylists(SpotifyWebAPI api)
        {
            List<FullPlaylist> ListPlaylist = new List<FullPlaylist>();

            FullPlaylist happy = api.GetPlaylist("6UWb73N46mrqracT0fVOK5");
            FullPlaylist focus = api.GetPlaylist("2mueU6IwvbnFiIMg98sn12");
            FullPlaylist stress = api.GetPlaylist("57epVGvvwVvYs87DL7ivYI");
            FullPlaylist edgy = api.GetPlaylist("4yGTrPfwDGkOfjMHkXQP4s");
            ListPlaylist.Add(happy);
            ListPlaylist.Add(focus);
            ListPlaylist.Add(stress);
            ListPlaylist.Add(edgy);
            return ListPlaylist;
        }

        public static List<FullPlaylist> CreatePLaylists(SpotifyWebAPI api) {
            Console.WriteLine("");

            Console.WriteLine("you are going to create playlists linked with your differents mood");
            Thread.Sleep(1000);
            Console.WriteLine("let's Start with the the playlist you would like to listen when you are happy");
            FullPlaylist happy = CreatePlaylist(api, "happy");
            Console.WriteLine("Create now your focus Playlist");
            FullPlaylist focus = CreatePlaylist(api,"focus");
            Console.WriteLine("Create now your stress Playlist");
            FullPlaylist stress = CreatePlaylist(api,"stress");
            Console.WriteLine("Create now your edgy PLaylist");
            FullPlaylist edgy = CreatePlaylist(api,"edgy");
            List<FullPlaylist> ListPlaylist = new List<FullPlaylist>();
            ListPlaylist.Add(happy);
            ListPlaylist.Add(focus);
            ListPlaylist.Add(stress);
            ListPlaylist.Add(edgy);
            return ListPlaylist;



            
        }

        public static FullPlaylist CreatePlaylist(SpotifyWebAPI api, String name) {

            PrivateProfile profile =  api.GetPrivateProfile();
            
            string id = profile.Id;
            Console.WriteLine(id);

            FullPlaylist playlist = api.CreatePlaylist("larchero", "test");
            Thread.Sleep(3000);
            api.AddPlaylistTrack(playlistId: playlist.Id, "spotify:track:2No1A7ZuMaBGxz45jmA9Gw" );
            Console.WriteLine(playlist.Name);

            



            
            while(true)
            {
                Console.WriteLine("Enter keyword to find a song, enter finish when you have done your playlist");
                string risposta = Console.ReadLine();
                if (risposta.Equals("finish"))
                {
                    Console.WriteLine("Your playlist is create and save");
                    break;


                }
                else
                {
                    if (api.SearchItems(risposta, SpotifyAPI.Web.Enums.SearchType.Track) == null)
                    {
                        Console.WriteLine("No Song found, enter an other keyword");
                        Thread.Sleep(3000);

                    }
                    else
                    {
                        SearchItem search = api.SearchItems(risposta, SpotifyAPI.Web.Enums.SearchType.Track);
                        Paging<FullTrack> ListTrack = search.Tracks;
                        int count = 0;
                        ListTrack.Items.ForEach(track =>
                        {

                            Console.WriteLine($"-{count}  {track.Name} by {track.Artists[0].Name}");

                            count++;

                        });

                        Console.WriteLine($"enter the number of the song you want to add to your playlist {name}" );

                        while(true)
                        {
                            int song = Convert.ToInt32(Console.ReadLine());
                            if (song <= ListTrack.Items.Count) {
                                api.AddPlaylistTrack(playlistId: playlist.Id, uri:  ListTrack.Items[song].Uri );
                                break;
                            }

                            else
                            {
                                Console.WriteLine("the number entered does not match any proposed music, please enter a correct number");
                            }
                        } 

                     
                        
                    }








            }
            } 

            return playlist;
            }



        public static List<FullPlaylist> LoadPlaylists(SpotifyWebAPI api)
        {
            List<FullPlaylist> ListPlaylist = new List<FullPlaylist>();
            FullPlaylist happy;
            FullPlaylist focus;
            FullPlaylist stress;
            FullPlaylist edgy;

            happy = LoadPlaylist(api, "happy");
            focus = LoadPlaylist(api, "focus");
            stress = LoadPlaylist(api, "stress");
            edgy = LoadPlaylist(api, "edgy");

            ListPlaylist.Add(happy);
            ListPlaylist.Add(focus);
            ListPlaylist.Add(stress);
            ListPlaylist.Add(edgy);
            
            return ListPlaylist;

        }

        public static FullPlaylist LoadPlaylist(SpotifyWebAPI api, string name)
        {
              FullPlaylist play = new FullPlaylist();
            string uri;

            //while (true)
            //{
            
            Console.WriteLine("Enter the URI of your " + name + " Playlist");
            Console.WriteLine("");
            try
            {
                uri = Console.ReadLine();
                play = api.GetPlaylist(uri);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
         
            
            return play;




            //if (api.GetPlaylist(uri).Uri == null)
            // {
            // Console.WriteLine("Enter a corect URI please");
            //}
            //else
            //{






            //break;
            //}


            //}

        }

            public static void ChangePlayback(SpotifyWebAPI api,FullPlaylist playlist)
        {
            String UriTrack = GetUriRandom(api, playlist);
            PlaybackContext play = api.GetPlayingTrack();
            api.SetVolume(50);
          
            
            api.ResumePlayback(deviceId : "",contextUri:playlist.Uri, uris: null, offset: ""); 

            
            
            




        }

        public static String GetUriRandom(SpotifyWebAPI api, FullPlaylist playlist)
        {
            int nb = playlist.Tracks.Items.Count;
            Random alea = new Random();
            int rand = alea.Next(nb);
            FullTrack track = playlist.Tracks.Items[rand].Track;
            String UriTrack = track.Id;
            return UriTrack;
        }


            public static FullPlaylist GetPlaylistMood(List<FullPlaylist> Playlists) {
            
            Prog.ReturnData();
            
            ArrayList data = Prog.Perf;
            
            Console.WriteLine("your Excitment value is: " + data[1]);
            Console.WriteLine("your stress value is: " + data[3]);
            Console.WriteLine("your relaxation value is: " + data[4]);
            Console.WriteLine("your focus value is: " + data[6]);

            double engagement =  Convert.ToDouble(data[0]);
            double excitement = Convert.ToDouble(data[1]); 
            double lexcitement = Convert.ToDouble(data[2]); 
            double stress = Convert.ToDouble(data[3]);
            double relaxation = Convert.ToDouble(data[4]);
            double interest = Convert.ToDouble(data[5]);
            double focus = Convert.ToDouble(data[6]);

            //Int32 max = Math.Max(stress, Math.Max(relaxation, excitement));

            //int stress = 1;
            //int relaxation = 1;
            //int excitement = 2;



            
            var max = Math.Max(stress, Math.Max(relaxation, Math.Max(excitement, focus)));
            



            if (max == stress)
            {
                Console.WriteLine("You are stressed");
                return Playlists[2];
            }
            else if (max == relaxation)
            {
                Console.WriteLine("You are relaxed" );
                return Playlists[0];
            }
            else if (max == excitement)
            {
                Console.WriteLine("You are excited" );
                return Playlists[3];
            }
            else if (max == focus)
            {
                Console.WriteLine("You are focused");
                return Playlists[1];
            }
            else
            {
                return null;
            }

        }
        
        public static void ManagePlayback(SpotifyWebAPI api)
        {
            Console.WriteLine("You are in the manage Playback mode now" );
            Thread.Sleep(2000);
            Console.WriteLine("Lift your head to increase the volume");
            Console.WriteLine("Lower your head to discrease the volume");
            Thread.Sleep(2000);
            Console.WriteLine("Turn your head to the right to skip the Playback to next");
            Console.WriteLine("Turn your head to the Left to skip the Playback to previous");
            Thread.Sleep(2000);

           // Console.WriteLine("would you be able to leave the App with closing your eyes 3 secondes ? (yes/no)");
            bool bre = false;
            int Vol = 50;


           
            //while(true)
            //{
                ///string risposta = Console.ReadLine();
                //if (risposta.Equals("Y") || risposta.Equals("yes") || risposta.Equals("y") || risposta.Equals("YES"))
                //{
                    //Console.WriteLine("Yes");

                    while (bre == false)
                    {


                        Vol = ChangeVolume(api, Vol);
                        SkipPlayback(api);
                        bre = LeaveApp(api, bre);



                        Thread.Sleep(250);






                    }






                    //break;
                //}
                //else if (risposta.Equals("N") || risposta.Equals("NO") || risposta.Equals("n") || risposta.Equals("no"))
                //{

                //    while (bre == false)
                //    {


                //        Vol = ChangeVolume(api, Vol);
                //        SkipPlayback(api);
                        



                //        Thread.Sleep(250);






                //    }

                //    break;
                //}

                //else
                //{
                //    Console.WriteLine("Enter yes or no please");
                //}

            //} 


            
            
           

             

            Prog.Dse.UnSubscribe();
            Thread.Sleep(5000);

            Prog.Dse.CloseSession();
            Thread.Sleep(5000);



        }


        public static void SkipPlayback(SpotifyWebAPI api)
        {
            //Console.WriteLine(Prog.Mot[6]);

            if (Convert.ToDouble(Prog.Mot[6]) < 7900)
            {

                api.SkipPlaybackToNext();
                PlaybackContext Music = api.GetPlayback();
                
                Console.WriteLine($"You are listening {Music.Item.Name} now!");
                
            }

            else if (Convert.ToDouble(Prog.Mot[6]) > 8750)
            {

                api.SkipPlaybackToPrevious();
               
                PlaybackContext Music2 = api.GetPlayback();
                Console.WriteLine($"You are listening {Music2.Item.Name} now!");
                
            }
            

            else { }
            Thread.Sleep(1000);

        }

        public static int ChangeVolume(SpotifyWebAPI api, int Vol)
        {
            if (Convert.ToDouble(Prog.Mot[7]) > 9700)
            {

                if (Vol < 100)
                {
                    api.SetVolume(Vol + 10);
                    Vol = Vol + 10;
                }

                else if (Vol >= 100)
                {
                    Console.WriteLine("Volume Max");
                }

            }

            else if (Convert.ToDouble(Prog.Mot[7]) < 7050)
            {

                if (Vol > 0)
                {
                    api.SetVolume(Vol - 10);
                    Vol = Vol - 10;
                }

                else if (Vol <= 0)
                {
                    Console.WriteLine("Volume Min");
                }

            }
            return Vol;
        }

        public static bool LeaveApp(SpotifyWebAPI api, bool bre)
        {
            Console.WriteLine(Prog.Fac[0]);
            if ((String)Prog.Fac[0] != "neutral")
            {
                Thread.Sleep(500);

                if ((String)Prog.Fac[0] != "neutral")

                    {
                        Console.WriteLine("End of the App");
                        api.PausePlayback();
                        


                       
                        return true;
                      
                    }
                


            }
            return false;
            
        }
           



        public static Token AddTocken(AuthorizationCodeAuth auth)
        {
            Token token = new Token();
            string url5 = "https://accounts.spotify.com/api/token";
            
            var clientid = "d959c53d82124f4a89b0b8f463152037";
            var clientsecret = "3605d0cde2d74c07822299c0359f840f";

            //request to get the access token
            var token2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientid, clientsecret)));

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization", "Basic " + token2);
            //webRequest.Headers.Add("Authorization: Basic " + encode_clientid_clientsecret);

            var request = ("grant_type=client_credentials");
            byte[] req_bytes = Encoding.ASCII.GetBytes(request);
            webRequest.ContentLength = req_bytes.Length;

            Stream strm = webRequest.GetRequestStream();
            strm.Write(req_bytes, 0, req_bytes.Length);
            strm.Close();

            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            String json = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    //should get back a string i can then turn to json and parse for accesstoken
                    json = rdr.ReadToEnd();
                    Console.WriteLine(json);
                    Console.WriteLine(token.AccessToken);

                    rdr.Close();
                    token = JsonConvert.DeserializeObject<Token>(json);
                    ;
                }
            }
            
            return token;
        }
    }
}
