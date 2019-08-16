## Usage

To find out the closest five address from a CSV file mention the CSV in Australia

##Instructions

This project is using the Google Geocoding service to find the address cooridnates.

Google API key should be enabled and all the required service should be enabled.

It won't work if daily usage limit is crossed.

##code

Using Helper class which is having static method like ReadCSV(),GetLatLongByAddress(), GetDistanceBetweenPoints() which is providing data to upper layer.


Service layer having two main method to find the closest distance from entered address and it is maintaining the latitude and longitude of addresses mentiond in the CSV file.

Web API method  => GetSearchResult(ouble latitude, double longitude)


##Test

Created a test to test the distance between two coordinates under ValuesContollerTest

function => DistanceBetweenCoordinates_ShouldEqual() (Successfull)

##Run

To run this project -> Ctrl+F5

##Limitations

This project is restricted to find the closest address in Australia.

It will give the flat surface distance instead of arial or by using any specific route


## Used

Google map service 

1) https://developers.google.com/maps/

Find distance between two coordinates

2) https://www.geodatasource.com/developers/c-sharp




