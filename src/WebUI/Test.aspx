<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<html>
<head>
<script src="js/jquery.1.3.2.js" type="text/javascript"></script>
<script src="js/test.js" type="text/javascript"></script>
</head>

<style type="text/css">
        .tree_skin
        {
            position: absolute;
            width: 60px;
            height: 20px;
            border:green solid 1px;
            left: 300px;
            top: 23px;
        }
         
        .tree_temp
        {
        	position: absolute;
            width: 60px;
            height: 20px;
            border:green solid 1px;
            left: 120px;
            top: 73px;
        }
        .line
        {
        	position:absolute;
        }
    </style>
<body>
    
    <div class="tree_skin" id="tree_skin">
      
    </div>
    
    <div class="tree_temp" id="tree_temp_1">
      
    </div>

</body>
</html>
