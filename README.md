# ZipCodeParser
This code allows to convert large JSON file containing all US zip codes with meta info to a bunch of JSON files where every files contains code and gero points for drawing only single zip region.


1) Clone this repository to your laptop/pc and open it in VS studio
2) Download geo-JSON file with entire list of US zip boundaries from opendatasoft page https://public.opendatasoft.com/explore/dataset/georef-united-states-of-america-zcta5 and unarchive it
3) Create a folder with name ```usa-zips``` in your Desktop folder
4) Put the geo-JSON file inside this folder and name it ```input-x.geojson```
5) Run the console application (it will start generating files per ZIP code and put them into ```output``` folder.

Now you can use these ZIP-code JSON files as you wish (e.g. to draw particular ZIP code boundaries on your MAP view)
