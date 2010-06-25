<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<html>
<head>
</head>
<style>
*{
margin:0px;
padding:0px;
}
#div1{
width:800px;
float:left;
background-color:#006633;
}
#div2{
width:100%;
float:left;
}
#div3{
width:100%;
float:left;
}
#div4{
display:none;
float:left;
}
#ul1 li{
float:left;
}
</style>
<body>
<div id="div1">
 <div id="div2">
     <ul id="ul1">
         <li onclick="fun(1);">选项一</li>
            <li onclick="fun(2);">选项二</li>
        </ul>
    </div>
    <div id="div3">
     <table>
         <tr>
             <td>1</td>
                <td>2</td>
                <td>3</td>
            </tr>
            <tr>
             <td>4</td>
                <td>5</td>
                <td>6</td>
            </tr>
            <tr>
             <td>7</td>
                <td>8</td>
                <td>9</td>
            </tr>
        </table>
    </div>
    <div id="div4">
     <p>测试测试</p>
    </div>
</div>

<script>
    function fun(a) {
        var div3 = document.getElementById('div3');
        var div4 = document.getElementById('div4');
        if (a == 1) {
            div3.style.display = "none";
            div4.style.display = "block";
        } else {
            div3.style.display = "block";
            div4.style.display = "none";
        }
    }
</script>

</body>
</html> 