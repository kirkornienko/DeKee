<%@ Page Language="C#" AutoEventWireup="true" CodeFile="03a_Card2Card__card_1.aspx.cs" Inherits="Pages_MoneyTransfer_03a_Card2Card__card_1" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Bank</title>

    <!-- Bootstrap -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700&subset=latin,cyrillic-ext' rel='stylesheet' type='text/css'>
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/flags.css" rel="stylesheet">
    <link href="css/bank-web.css" rel="stylesheet">
    <link href="css/card.css" rel="stylesheet">
    <link href="css/chartist.min.css" rel="stylesheet">
    <script src="js/chartist.min.js"></script>

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
  </head>
  <body class="bank-web">
    <nav class="navbar navbar-default bank-header">
      <div class="container-fluid">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="01_index.htm">Bank logo</a>
        </div>

        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="navbar-collapse collapse" id="bs-example-navbar-collapse-1" aria-expanded="false" style="height: 1px;">
          <ul class="nav navbar-nav">
            <li><a href="index.htm"><i class="fa fa-bar-chart"></i>My Finance</a></li>
            <li><a href="06_Transaction_history.aspx"><i class="fa fa-history"></i>Transactions History</a></li>
            <li><a href="07_Chat.htm"><i class="fa fa-comments-o"></i>Chat <span class="badge">4</span></a></li>
          </ul>
          <div class="navbar-text navbar-right bank-user-deck">
              <a href="02_registration.htm" class="user-logo pull-right hidden-sm hidden-xs"><i class="fa fa-user"></i></a>
              <div class="user-name-time">
                <div class="user-name-row">Logined as <a href="02_registration.htm">User Name</a></div>
                <div class="user-in-system">Time in system: 12:46</div>
              </div>
              <div class="header-langs hidden-sm">
                  <div class="header-lng active">RU</div>
                  <div class="header-lng">DE</div>
                  <div class="header-lng">ENG</div>
              </div>
            </div>
        </div><!-- /.navbar-collapse -->
      </div><!-- /.container-fluid -->
    </nav>

    <div class="container bank-main">
        <h2 class="secondary-title">
          <div class="icon-wrapper"><i class="fa fa-credit-card"></i></div> Card 2 Card
        </h2>
        <ol class="breadcrumb card-flow">
          <li><a href="#">1: Enter your card info</a></li>
          <li>2: Check information</li>
          <li>3: Pay</li>
        </ol>


        <form class="form-horizontal card-2card-form" runat="server" id="container">
            <div class="form-group">
              <label class="col-sm-3 control-label">Your card:</label>
              <div class="col-sm-9">
                  <asp:DropDownList runat="server" ID="Sender" CssClass="form-control">
                    <asp:ListItem Text="5299640000136129" Value="5299640000136129" />
                    <asp:ListItem Text="5555444433332222" Value="5555444433332222" />
                  </asp:DropDownList>

                <%--<select class="form-control">
                  <option>5001 0000 0000 0000</option>
                  <option>5002 0000 0000 0000</option>
                  <option>5003 0000 0000 0000</option>
                </select>--%>
              </div>
            </div>
            <div class="form-group">
              <label class="col-sm-3 control-label">Recharge card:</label>
              <div class="col-sm-9">
                <asp:TextBox runat="server" ID="Recepient" Text="5299640000136145" CssClass="form-control" />
                <%--<input type="text" class="form-control" id="rechange-card" placeholder="Recharge card">--%>
              </div>
            </div>
            <div class="form-group">
              <label class="col-sm-3 control-label">For payment:</label>
              <div class="col-sm-9">
                <asp:TextBox runat="server" TextMode="MultiLine" ID="Description" Text="Test Desription" CssClass="form-control" />
                <%--<textarea class="form-control" placeholder="Put your payment ditails here"></textarea>--%>
              </div>
            </div>
            <div class="form-group">
              <label class="col-sm-3 control-label">Amount:</label>
              <div class="col-sm-4">
                <asp:TextBox runat="server" ID="Amount" CssClass="form-control sum-form" />
                <%--<input type="text" class="form-control sum-form" placeholder="">--%>
              </div>
            </div>
            <div class="form-group">
              <label class="col-sm-3 control-label">CVV:</label>
              <div class="col-sm-9">
                <asp:TextBox runat="server" ID="CVV" TextMode="Password" CssClass="form-control cvv-form" />
                <%--<input type="text" class="form-control cvv-form" placeholder="">--%>
              </div>
            </div>
            <div class="form-group">
              <div class="col-sm-offset-3 col-sm-8">
                <asp:Button runat="server" ID="Resume" Text="Resume" CssClass="btn btn-success" OnClick="Resume_OnClick" />
                <%--<a href="03a_Card2Card__card_2.aspx" type="submit" class="btn btn-success">Resume</a>--%>
              </div>
            </div>
        </form>



    </div>
    <footer class="bank-footer">
       <div class="container">
         <div class="col-xs-12 col-sm-6 col-md-4">
            <h4>Currency exchenge:</h4>
            <div class="footer-currency">
              <div class="footer-currency__item">
                <strong>USD/EUR:</strong> <span class="footer-currency__val">28/30</span>
              </div>
              <div class="footer-currency__item">
                <strong>GBP/EUR:</strong> <span class="footer-currency__val">28/30</span>
              </div>
              <div class="footer-currency__item">
                <strong>GBP/EUR:</strong> <span class="footer-currency__val">28/30</span>
              </div>
            </div>
          </div>
          <div class="col-xs-12 col-sm-6 col-md-8">
            <h4>Address and contact:</h4>
            <div class="footer-contacts-line address-line">London UK, The Business Centre 61 Wellfield Road
            <a href="011_Geolocation.htm">Geolocation link</a></div>
            <div class="footer-contacts-line"><i class="fa fa-phone"></i> +8 (099) 667 77 88</div>
            <div class="footer-contacts-line"><i class="fa fa-envelope"></i> <a href="#">email@bank.com</a></div>
          </div>
        </div>
        <div class="copy-text">
          <div class="container">&copy; 2015 Bank name, Inc. All rights reserved.</div>
        </div>
    </footer>

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="js/jquery-1.11.2.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="js/bootstrap.min.js"></script>
    <script>
        var data = {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'Mai', 'Jun'],
            series: [
            [5, 4, 3, 7, 5, 10, 3, 4, 8, 10, 6, 8],
            [3, 2, 9, 5, 4, 6, 4, 6, 7, 8, 7, 4]
          ]
        };

        var options = {
            seriesBarDistance: 15
        };

        var responsiveOptions = [
          ['screen and (min-width: 641px) and (max-width: 1024px)', {
              seriesBarDistance: 10,
              axisX: {
                  labelInterpolationFnc: function (value) {
                      return value;
                  }
              }
          }],
          ['screen and (max-width: 640px)', {
              seriesBarDistance: 5,
              axisX: {
                  labelInterpolationFnc: function (value) {
                      return value[0];
                  }
              }
          }]
        ];

        new Chartist.Bar('.ct-chart', data, options, responsiveOptions);
    </script>
  </body>
</html>