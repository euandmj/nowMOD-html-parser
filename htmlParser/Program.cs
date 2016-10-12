using System;
using System.Collections.Generic;
using System.Net;
using System.IO; 

namespace htmlParser
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string nameId = "<h1 id=\"ctl00_cntrlMainContentPlaceHolder_txtContactName\">";
            string attrName = "_txtAttributeName";
            string attrVal = "_txtAttributeValue";
            string imageId = "_pnlItem";
            
            string Url = "http://www.nowmodels.co.uk/models/Aaron--Neville/299.aspx";

            string[] allUrls = GetAllUrls();
            
            for (int j = 0; j < allUrls.Length; j++)
            {
                
                    
                string html = new System.Net.WebClient().DownloadString(allUrls[j]);
                string modelName = GetModelName(nameId, html);
                string[] modelFirstName = modelName.Split(' '); 
                var indexOfAttrIds = html.GetAllIndexOfNames(attrName, StringComparison.OrdinalIgnoreCase);
                string[] names = GetAttrNameIds(indexOfAttrIds, html); // Array of all the name id's. 
                string[] vals = GetAttrValueIds(indexOfAttrIds, html); // Array of all the value id's. 
                string[,] info = CompileInfo(names, vals, html); // Arranges the info in matching order of attribute name and value. 
                var indexOfImages = html.GetAllIndexOfImages(imageId, StringComparison.OrdinalIgnoreCase); // finds every index of the image identifier. 
                string[] imageIds = GetImageIds(indexOfImages, html);

                string localPath = @"D:\documents\School Work\Now Models\script\" + modelName + "\\";

                Console.WriteLine(modelName);

                System.IO.Directory.CreateDirectory(@"D:\documents\School Work\Now Models\script" + @"\" + modelName);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(localPath + @"\" + modelName + ".txt", true))
                {
                    file.WriteLine(modelName);
                    for (int i = 0; i < names.Length; i++)
                    {
                        string str = info[i, 0] + "\n" + info[i, 1] + "\n\n";
                        {
                            file.WriteLine(str);                          
                        }
                    }
                }
                DownloadImages(imageIds, html, localPath, modelFirstName);
            }

            
            Console.Write("Completed. Press any key to close."); 
            Console.ReadKey(); 


        }

        public static string[] GetAllUrls()
        {
            string men = @"http://www.nowmodels.co.uk/models/Aaron--Neville/299.aspx;http://www.nowmodels.co.uk/models/Adrian-James/72.aspx;http://www.nowmodels.co.uk/models/Andrew-Gordon/79.aspx;http://www.nowmodels.co.uk/models/Anton-Dean/83.aspx;http://www.nowmodels.co.uk/models/Brian-Povlsen/92.aspx;http://www.nowmodels.co.uk/models/Charles-Turner/292.aspx;http://www.nowmodels.co.uk/models/Charles-Towning/289.aspx;http://www.nowmodels.co.uk/models/Chris-Poole/100.aspx;http://www.nowmodels.co.uk/models/Christian-Dalton/101.aspx;http://www.nowmodels.co.uk/models/Dave-Norwell/107.aspx;http://www.nowmodels.co.uk/models/Graham-Gardener/124.aspx;http://www.nowmodels.co.uk/models/Jack-Revell/294.aspx;http://www.nowmodels.co.uk/models/James-Brown/136.aspx;http://www.nowmodels.co.uk/models/Jesse-Bradbury/144.aspx;http://www.nowmodels.co.uk/models/Kevin-Dixon/290.aspx;http://www.nowmodels.co.uk/models/Lloyd-Nwagboso/282.aspx;http://www.nowmodels.co.uk/models/Luc-Jantschek/174.aspx;http://www.nowmodels.co.uk/models/Matt-Bailey/181.aspx;http://www.nowmodels.co.uk/models/Matt-Bates/182.aspx;http://www.nowmodels.co.uk/models/Michael-Addo/283.aspx;http://www.nowmodels.co.uk/models/Mohan-Randhawa/186.aspx;http://www.nowmodels.co.uk/models/Paul-Woods/196.aspx;http://www.nowmodels.co.uk/models/Paul-Allen/286.aspx;http://www.nowmodels.co.uk/models/Phil-Morris/199.aspx;http://www.nowmodels.co.uk/models/Richard-Kyte/205.aspx;http://www.nowmodels.co.uk/models/Scott-Campbell/217.aspx;http://www.nowmodels.co.uk/models/Sean-Hopwood/218.aspx;http://www.nowmodels.co.uk/models/Stephen-Walker/284.aspx;http://www.nowmodels.co.uk/models/Stuart-Reed/226.aspx;http://www.nowmodels.co.uk/models/Terry-Dormer/280.aspx;http://www.nowmodels.co.uk/models/Tony-Dowding/291.aspx";
            string women = @"http://www.nowmodels.co.uk/models/Abbey-Saunders/70.aspx;http://www.nowmodels.co.uk/models/Alison-Cain/75.aspx;http://www.nowmodels.co.uk/models/Amanda-Robbins/76.aspx;http://www.nowmodels.co.uk/models/Anastasija-Bogatirjova/263.aspx;http://www.nowmodels.co.uk/models/Anete-Vilcane/82.aspx;http://www.nowmodels.co.uk/models/Anna-Carradice/278.aspx;http://www.nowmodels.co.uk/models/Becky-Guest/88.aspx;http://www.nowmodels.co.uk/models/Bridget-Pickerill/288.aspx;http://www.nowmodels.co.uk/models/Brigitte-Suligoj/93.aspx;http://www.nowmodels.co.uk/models/Camilla-Tranter/95.aspx;http://www.nowmodels.co.uk/models/Candida-Yeldham/302.aspx;http://www.nowmodels.co.uk/models/Charley--Rose/273.aspx;http://www.nowmodels.co.uk/models/Charlotte-Atkinson/298.aspx;http://www.nowmodels.co.uk/models/Claire-Pope/103.aspx;http://www.nowmodels.co.uk/models/Claire-Guest/102.aspx;http://www.nowmodels.co.uk/models/Daniela-Isherwood/260.aspx;http://www.nowmodels.co.uk/models/Danielle-Holbrook/279.aspx;http://www.nowmodels.co.uk/models/Danni-Menzies/293.aspx;http://www.nowmodels.co.uk/models/Destiny-Sedlacek/268.aspx;http://www.nowmodels.co.uk/models/Donna-Louise-Bryan/259.aspx;http://www.nowmodels.co.uk/models/Emma-Rice/114.aspx;http://www.nowmodels.co.uk/models/Erica-Jane/269.aspx;http://www.nowmodels.co.uk/models/Fiona-Lamb/281.aspx;http://www.nowmodels.co.uk/models/Frances-Wingate/118.aspx;http://www.nowmodels.co.uk/models/Freya-Berry/120.aspx;http://www.nowmodels.co.uk/models/Gail-Shuttleworth/121.aspx;http://www.nowmodels.co.uk/models/Gemma--Rhodes/261.aspx;http://www.nowmodels.co.uk/models/Georgia-Danielle-Samuels/270.aspx;http://www.nowmodels.co.uk/models/Hannah-Sanders/128.aspx;http://www.nowmodels.co.uk/models/Hollie-McGarry/130.aspx;http://www.nowmodels.co.uk/models/Ines-Gray/131.aspx;http://www.nowmodels.co.uk/models/Jeannie-Frith/140.aspx;http://www.nowmodels.co.uk/models/Jenis-Adams/142.aspx;http://www.nowmodels.co.uk/models/Jenny-Kennard/143.aspx;http://www.nowmodels.co.uk/models/Jessica--Munro/274.aspx;http://www.nowmodels.co.uk/models/Jo-Lawden/147.aspx;http://www.nowmodels.co.uk/models/Josephine-McGrail/300.aspx;http://www.nowmodels.co.uk/models/Jules-Wheeler/152.aspx;http://www.nowmodels.co.uk/models/Juste-Juozapaityte/262.aspx;http://www.nowmodels.co.uk/models/Karen-Meyerhoff/156.aspx;http://www.nowmodels.co.uk/models/Karen-Koramshai/271.aspx;http://www.nowmodels.co.uk/models/Kate-Loustau/160.aspx;http://www.nowmodels.co.uk/models/Katherine-Russell/161.aspx;http://www.nowmodels.co.uk/models/Katie-Carne/163.aspx;http://www.nowmodels.co.uk/models/Kaye-Fletcher/165.aspx;http://www.nowmodels.co.uk/models/Kerry-Pace/166.aspx;http://www.nowmodels.co.uk/models/Kirsten-Ringlemann/168.aspx;http://www.nowmodels.co.uk/models/Kirsty-Dunning/169.aspx;http://www.nowmodels.co.uk/models/Laura-Jean-Marsh/297.aspx;http://www.nowmodels.co.uk/models/Linda--Soares/267.aspx;http://www.nowmodels.co.uk/models/Lucy-Lowe/175.aspx;http://www.nowmodels.co.uk/models/Louise-Tarver/173.aspx;http://www.nowmodels.co.uk/models/Lisa-Kerr/172.aspx;http://www.nowmodels.co.uk/models/Marie-Francoise-Wolff/255.aspx;http://www.nowmodels.co.uk/models/Melanie-Palma/301.aspx;http://www.nowmodels.co.uk/models/Monica-McDermott/187.aspx;http://www.nowmodels.co.uk/models/Nicola-Fletcher/276.aspx;http://www.nowmodels.co.uk/models/Nina-Malone/190.aspx;http://www.nowmodels.co.uk/models/Mamta-Bhatia/176.aspx;http://www.nowmodels.co.uk/models/Olivia-Ferrer/191.aspx;http://www.nowmodels.co.uk/models/Pearl-Hancock/198.aspx;http://www.nowmodels.co.uk/models/Poppy-Armitage/201.aspx;http://www.nowmodels.co.uk/models/Rachel-Walker/258.aspx;http://www.nowmodels.co.uk/models/Rebecca-Howard/248.aspx;http://www.nowmodels.co.uk/models/Rosie-Edwards/209.aspx;http://www.nowmodels.co.uk/models/Sandra-Darnell/251.aspx;http://www.nowmodels.co.uk/models/Sarah-Hall/215.aspx;http://www.nowmodels.co.uk/models/Sarah-Faulkner/214.aspx;http://www.nowmodels.co.uk/models/Sarah-Hannon/216.aspx;http://www.nowmodels.co.uk/models/Sheri--Staplehurst/257.aspx;http://www.nowmodels.co.uk/models/Sophie-Page/256.aspx;http://www.nowmodels.co.uk/models/Stacey-Ross/223.aspx;http://www.nowmodels.co.uk/models/Sue-Farrer/228.aspx;http://www.nowmodels.co.uk/models/Tess-Jantschek/234.aspx;http://www.nowmodels.co.uk/models/Vanessa-Calderone/239.aspx;http://www.nowmodels.co.uk/models/Vicki-Murdoch/240.aspx;http://www.nowmodels.co.uk/models/Vicky-Boateng/241.aspx";
            string classic_men = @"http://www.nowmodels.co.uk/models/Alex-Cameron/74.aspx;http://www.nowmodels.co.uk/models/Amejit-Deu/77.aspx;http://www.nowmodels.co.uk/models/Andrew-Swateridge/81.aspx;http://www.nowmodels.co.uk/models/Bryan-Lawrence/94.aspx;http://www.nowmodels.co.uk/models/David-Jenkins/110.aspx;http://www.nowmodels.co.uk/models/David-Doma/108.aspx;http://www.nowmodels.co.uk/models/David-Habbin/109.aspx;http://www.nowmodels.co.uk/models/Jeremy-Vinogradov/265.aspx;http://www.nowmodels.co.uk/models/John-Nichols/247.aspx;http://www.nowmodels.co.uk/models/John-Bennett/149.aspx;http://www.nowmodels.co.uk/models/Kevin-Smith/167.aspx;http://www.nowmodels.co.uk/models/Mark-Lewis/178.aspx;http://www.nowmodels.co.uk/models/Martin-Valton/180.aspx;http://www.nowmodels.co.uk/models/Matt-Carey/183.aspx;http://www.nowmodels.co.uk/models/Mark-Heard/266.aspx;http://www.nowmodels.co.uk/models/Miles-Alltoft/287.aspx;http://www.nowmodels.co.uk/models/Nigel--Barnfield/252.aspx;http://www.nowmodels.co.uk/models/Paul-Kerry/195.aspx;http://www.nowmodels.co.uk/models/Simon-Spalding/220.aspx;http://www.nowmodels.co.uk/models/Simon-Smith/219.aspx;http://www.nowmodels.co.uk/models/Steve-Bullock/225.aspx;http://www.nowmodels.co.uk/models/Tony-Stewart/237.aspx;http://www.nowmodels.co.uk/models/Will-Fawcett/243.aspx";
            string classic_women = @"http://www.nowmodels.co.uk/models/Ann-Jackson/254.aspx;http://www.nowmodels.co.uk/models/Brigitta-Scholz/264.aspx;http://www.nowmodels.co.uk/models/Carrie-Cooper/99.aspx;http://www.nowmodels.co.uk/models/Danielle-Russell/106.aspx;http://www.nowmodels.co.uk/models/Debbie-Cameron/111.aspx;http://www.nowmodels.co.uk/models/Deborah-Pollington/113.aspx;http://www.nowmodels.co.uk/models/Helen-Lowe/129.aspx;http://www.nowmodels.co.uk/models/Jacqueline-Matty/133.aspx;http://www.nowmodels.co.uk/models/Janis-Ahern/253.aspx;http://www.nowmodels.co.uk/models/Jo-Cain/146.aspx;http://www.nowmodels.co.uk/models/Joanna-Fussey/148.aspx;http://www.nowmodels.co.uk/models/Juliana-Formicola/295.aspx;http://www.nowmodels.co.uk/models/Karen-ONeill/157.aspx;http://www.nowmodels.co.uk/models/Kathy-Hill/162.aspx;http://www.nowmodels.co.uk/models/Linda-Walton/171.aspx;http://www.nowmodels.co.uk/models/Lara-James/170.aspx;http://www.nowmodels.co.uk/models/Julie-Lowery/153.aspx;http://www.nowmodels.co.uk/models/Kay-Bonetti/164.aspx;http://www.nowmodels.co.uk/models/Nicola-Mace/189.aspx;http://www.nowmodels.co.uk/models/Pat-Perse-White/193.aspx;http://www.nowmodels.co.uk/models/Marissa-Charles-Wagg/177.aspx;http://www.nowmodels.co.uk/models/Resi-Harris/203.aspx;http://www.nowmodels.co.uk/models/Sally-Way/213.aspx;http://www.nowmodels.co.uk/models/Sarah-Stacey/272.aspx;http://www.nowmodels.co.uk/models/Sue-Scadding/230.aspx;http://www.nowmodels.co.uk/models/Sue-Clarke/227.aspx;http://www.nowmodels.co.uk/models/Stephanie-Worrall/224.aspx;http://www.nowmodels.co.uk/models/Sushma-Pugalia/231.aspx;http://www.nowmodels.co.uk/models/Sue-McCartney/229.aspx;http://www.nowmodels.co.uk/models/Suzanne-Younger/232.aspx;http://www.nowmodels.co.uk/models/Tracey-Lushington/277.aspx;http://www.nowmodels.co.uk/models/Tiffany-Suchard/236.aspx";
            string superclassic_men = @"http://www.nowmodels.co.uk/models/Alan-Jones/73.aspx;http://www.nowmodels.co.uk/models/Barry-Rohde/87.aspx;http://www.nowmodels.co.uk/models/Bert-Newsome/89.aspx;http://www.nowmodels.co.uk/models/Colin-Childs/104.aspx;http://www.nowmodels.co.uk/models/David-Matthews/250.aspx;http://www.nowmodels.co.uk/models/Eric-Stack/117.aspx;http://www.nowmodels.co.uk/models/Greg-Sherriff/127.aspx;http://www.nowmodels.co.uk/models/Jeff-Jeffries/141.aspx;http://www.nowmodels.co.uk/models/John-Dakin/150.aspx;http://www.nowmodels.co.uk/models/Paul-Blake/194.aspx;http://www.nowmodels.co.uk/models/Michael-Ross/184.aspx;http://www.nowmodels.co.uk/models/Mike-Nelson/185.aspx;http://www.nowmodels.co.uk/models/Robbie-Charles/207.aspx;http://www.nowmodels.co.uk/models/Robert-Lord/208.aspx;http://www.nowmodels.co.uk/models/Timothy-Ahern/246.aspx;http://www.nowmodels.co.uk/models/Russell-Kilmister/212.aspx";
            string superclassic_women = @"http://www.nowmodels.co.uk/models/Avril-Shepherd/85.aspx;http://www.nowmodels.co.uk/models/Barbara-Whatley/86.aspx;http://www.nowmodels.co.uk/models/Carole-Anne-Goodman/97.aspx;http://www.nowmodels.co.uk/models/Carolyn-Pertwee/98.aspx;http://www.nowmodels.co.uk/models/Debbie-Grieve/112.aspx;http://www.nowmodels.co.uk/models/Francine-Bloom/119.aspx;http://www.nowmodels.co.uk/models/Jacqui-Shirtliff/135.aspx;http://www.nowmodels.co.uk/models/Jane-Russell/137.aspx;http://www.nowmodels.co.uk/models/Jacqueline-Shaw/134.aspx;http://www.nowmodels.co.uk/models/Jay-Schofield/139.aspx;http://www.nowmodels.co.uk/models/June-Roscoe/154.aspx;http://www.nowmodels.co.uk/models/Jane-Famous/285.aspx;http://www.nowmodels.co.uk/models/Pam-Porter/192.aspx;http://www.nowmodels.co.uk/models/Pauline-Howard/197.aspx;http://www.nowmodels.co.uk/models/Kate-Hammersley/159.aspx;http://www.nowmodels.co.uk/models/Wendy-Marshall/242.aspx;http://www.nowmodels.co.uk/models/Tessa-Landers/235.aspx";

            Console.WriteLine("(1) - men\n(2) - women\n(3) - classic men\n(4) - classic women\n(5) - superclassic men\n(6) - superclassic women");
            int x = int.Parse(Console.ReadLine());

            switch (x)
            {
                case 1:
                    return men.Split(';');
                case 2:
                    return women.Split(';');
                case 3:
                    return classic_men.Split(';');
                case 4: 
                    return classic_women.Split(';');
                case 5: 
                    return superclassic_men.Split(';');
                case 6: 
                    return superclassic_women.Split(';');
                default:
                    return men.Split(';');
            }
        }

        public static string GetModelName(string Id, string Body)
        {
            int nameIdIndex = Body.IndexOf(Id);
            int nameIdIndexEnd = Body.IndexOf("</h1>", nameIdIndex);
            string firstCut = Body.Substring(nameIdIndex, (nameIdIndexEnd - nameIdIndex));
            int index = firstCut.IndexOf(Id);
            return firstCut.Substring(index + Id.Length);
        }

        public static IList<int> GetAllIndexOfNames(this string attrName, string Body, StringComparison comparison)
        {
            IList<int> allIndexOf = new List<int>();
            int index = attrName.IndexOf(Body, comparison);
            while (index != -1)
            {
                allIndexOf.Add(index);
                index = attrName.IndexOf(Body, index + Body.Length, comparison);
            }
            return allIndexOf;
        }

        public static IList<int> GetAllIndexOfImages(this string attrName, string Body, StringComparison comparison)
        {
            IList<int> allIndexOf = new List<int>();
            int index = attrName.IndexOf(Body, comparison);
            while (index != -1)
            {
                allIndexOf.Add(index);
                index = attrName.IndexOf(Body, index + Body.Length, comparison);
            }
            return allIndexOf;  
        }

        public static string[] GetAttrNameIds(IList<int> allIndexOf, string Body)
        {
            string[] array = new string[allIndexOf.Count]; 
            // Gets an array of each id name. ContactAttribute IDs ascend by two. 
            for (int i = 0; i < allIndexOf.Count; i++)
            {
                if(i < 5)
                {
                    array[i] = "ctl00_cntrlMainContentPlaceHolder_rptContactAttributes_ctl0" + (i * 2).ToString() + "_txtAttributeName";
                }
                else
                {
                    array[i] = "ctl00_cntrlMainContentPlaceHolder_rptContactAttributes_ctl" + (i * 2).ToString() + "_txtAttributeName";
                }                    
            }
           
            return array; 
        }

        public static string[] GetAttrValueIds(IList<int> allIndexOf, string Body)
        {
            string[] array = new string[allIndexOf.Count];

            for (int i = 0; i < allIndexOf.Count; i++)
            {
                if (i < 5)
                    array[i] = "ctl00_cntrlMainContentPlaceHolder_rptContactAttributes_ctl0" + (i * 2).ToString() + "_txtAttributeValue";
                else
                    array[i] = "ctl00_cntrlMainContentPlaceHolder_rptContactAttributes_ctl" + (i * 2).ToString() + "_txtAttributeValue";
            }

            return array; 

        }

        public static string[] GetImageIds(IList<int> allIndexOf, string Body)
        {
            string[] array = new string[allIndexOf.Count];
            for (int i = 0; i < allIndexOf.Count; i++)
            {
                array[i] = "ctl00_cntrlMainContentPlaceHolder_cntrlAdditionalPhotos_i" + i.ToString() + "_pnlItem"; 
            }
            return array; 
        }        

        public static void DownloadImages(string[] ImageIds, string Body, string LocalPath, string[] ModelName)
        {
            // Procedure will find the link and then follow it up and download to local disk. 
            string[] links = new string [ImageIds.Length]; 

            for(int i = 0; i < ImageIds.Length; i++)
            {
                int imageIdIndex = Body.IndexOf(ImageIds[i]);
                int beforeLink = Body.IndexOf(".src=", imageIdIndex);
                beforeLink += 5;
                int afterLink = Body.IndexOf("'", beforeLink);
                links[i] = Body.Substring(beforeLink + 1, 137);
                int zeroByteFiles = 0; 

                using (WebClient client = new WebClient())
                {
                    if(i == 0)
                        client.DownloadFileAsync(new Uri(links[i]), LocalPath + ModelName[0] + ".jpg");
                    else
                    {
                        if (i % 10 == 0)
                        {
                            Console.WriteLine("Waiting 5s");
                            System.Threading.Thread.Sleep(5000);                            
                        }
                        client.DownloadFileAsync(new Uri(links[i]), LocalPath + i + ".jpg");
                    }
                        
                }

                // Checks if any of the files downloaded are 0bytes. 
                string[] allFiles = Directory.GetFiles(LocalPath);
                foreach(string file in allFiles)
                {
                    long length = new FileInfo(file).Length;
                    if (length <= 0)
                        zeroByteFiles++; 
                }
                if (zeroByteFiles == 0)
                    Console.WriteLine(zeroByteFiles + " image download errors occured. id: " + ModelName); 
            }            
        }

        public static string[,] CompileInfo(string[] nameIds, string[] valueIds, string Body)
        {
            string[,] info = new string[nameIds.Length, nameIds.Length];

            for(int i = 0; i < nameIds.Length; i++)
            {
                // Gets the attributeName. 
                int nameId1Index = Body.IndexOf(nameIds[i], nameIds[i].Length + 23); // Index of the end of the substring before the content. 
                int nameId1Index2 = Body.IndexOf("</span>", nameId1Index); // Index of the character after end of content.
                nameId1Index2 -= 1;
                info[i, 0] = Body.Substring(nameId1Index + 1, (nameId1Index2 - (nameId1Index))); // cuts to after the content. 
                info[i, 0] = info[i, 0].Substring(info[i, 0].IndexOf("teName\">") + 8); // Cuts the id tag from before the content. 

                // Gets the attributeValue. (1 char longer than attributeName)
                int valueId1Index = Body.IndexOf(valueIds[i], valueIds[i].Length + 23);
                int valueId1Index2 = Body.IndexOf("</span>", valueId1Index);
                valueId1Index2 -= 1;
                info[i, 1] = Body.Substring(valueId1Index + 1, (valueId1Index2 - (valueId1Index)));
                info[i, 1] = info[i, 1].Substring(info[i, 1].IndexOf("teValue\">") + 9);

                // Remove the "&quot;" from the height value. 
                if(info[0, 1].IndexOf("&quot;") != -1)
                {
                    info[0, 1] = info[0, 1].Remove(info[0, 1].IndexOf("&quot;"), 6); 
                }

            }            
            return info;  
        }

    }
}
