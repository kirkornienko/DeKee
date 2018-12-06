<%@ Page Language="C#" AutoEventWireup="true" CodeFile="06_Transaction_history.aspx.cs" Inherits="Pages_MoneyTransfer_06_Transaction_history" %>

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
            <li class="active"><a href="#"><i class="fa fa-history"></i>Transactions History</a></li>
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
          <div class="icon-wrapper"><i class="fa fa-history"></i></div>Transactions history
        </h2>

        <div class="row">
          <div class="col-md-3">
              <label>Select your card</label>
              <select class="form-control choose-card-select">
                <option value="mc">5001 0000 0000 0000</option>
                <option value="visa">5002 0000 0000 0000</option>
                <option value="ae">5003 0000 0000 0000</option>
              </select>
              <div class="card-preview">
                <div class="card-preview__placeholder text-right card-mc" style="display: block;">
                  <img src="img/card_1.png">
                </div>
                <div class="card-preview__placeholder text-right card-visa">
                  <img src="img/card_2.png">
                </div>
                <div class="card-preview__placeholder text-right card-ae">
                  <img src="img/card_3.png">
                </div>
              </div>
          </div>
          <div class="col-md-9">
             <form class="form-horizontal filter-transactions clearfix">
              <div class="form-group col-md-3">
                <label>Select from date</label>
                <input type="text" class="form-control" id="transfer-from-date" placeholder="From date">
              </div>
              <div class="filter-transactions-separator">-</div>
              <div class="form-group col-md-3">
                <label>Select to date</label>
                <input type="text" class="form-control" id="transfer-to-date" placeholder="To date">
              </div>
              <div class="form-group col-md-4">
               <label>Income/Expenses</label>
               <div class="btn-group" role="group" aria-label="...">
                <button type="button" class="btn btn-default">Positive</button>
                <button type="button" class="btn btn-default">Negative</button>
                <button type="button" class="btn btn-default active">All</button>
              </div>
              </div>
              <div class="form-group col-md-2">
               <label>Category</label>
               <select class="form-control">
                 <option>Card2card</option>
                 <option>Bank transfer</option>
                 <option>Mobile</option>
               </select>
              </div>
              <div class="form-group col-md-3">
                <label>Amount from</label>
                <input type="text" class="form-control" id="transfer-from-date" placeholder="From amount">
              </div>
              <div class="filter-transactions-separator">-</div>
              <div class="form-group col-md-3">
                <label>Amount to</label>
                <input type="text" class="form-control" id="transfer-to-date" placeholder="To amount">
              </div>

              <div class="form-group col-md-2">
               <label>Currency</label>
               <select class="form-control">
                 <option>USD</option>
                 <option>EUR</option>
                 <option>RUB</option>
               </select>
              </div>
              <div class="form-group col-md-4 text-right apply-filter">
               <button type="submit" class="btn btn-success text-rigth">Apply Filters</button>
              </div>
            </form>
            <div class="list-group transactions-list">
              <a href="#" class="list-group-item">
                <div class="pull-left">
                  <h4 class="list-group-item-heading">e-bay shoping</h4>
                  <p class="list-group-item-text">13.6.2015</p>
                </div>
                <span class="transactions-list__price negative">-34 USD</span>
              </a>
              <a href="#" class="list-group-item">
                <div class="pull-left">
                <h4 class="list-group-item-heading">Buy fruits in Silpo market</h4>
                <p class="list-group-item-text">15.6.2015</p>
                </div>
                <span class="transactions-list__price negative">-34 USD</span>
              </a>
              <a href="#" class="list-group-item">
                <div class="pull-left">
                <h4 class="list-group-item-heading">Buy fruits in Silpo market</h4>
                <p class="list-group-item-text">15.6.2015</p>
                </div>
                <span class="transactions-list__price negative">-34 USD</span>
              </a>
              <a href="#" class="list-group-item">
                <div class="pull-left">
                <h4 class="list-group-item-heading">Buy fruits in Silpo market</h4>
                <p class="list-group-item-text">15.6.2015</p>
                </div>
                <span class="transactions-list__price positive">795 USD</span>
              </a>
              <a href="#" class="list-group-item">
                <div class="pull-left">
                <h4 class="list-group-item-heading">Buy fruits in Silpo market</h4>
                <p class="list-group-item-text">15.6.2015</p>
                </div>
                <span class="transactions-list__price negative">-34 USD</span>
              </a>
            </div>
          </div>
        </div>


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
    <script src="js/main.js"></script>
  </body>
</html>