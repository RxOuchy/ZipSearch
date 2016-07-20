# ZipSearch
Just messing around with a form app to allow someone to search through massive amounts of zip archives


Archive Structure

-Service Directory
  -Archive Directory
    -Zip Files (100K+)
      -ZipFileContents (100+ Directorys using Client GUID)
        -ClientGUID Directory Contents (Couple of TEXT based files (Either .txt or .csv ext typically)
          -File (Name Usually products_1_DATETIMESTAMP.csv) [The _1 changes if more than 1 file is uploaded in a cycle <15min cycle>]
