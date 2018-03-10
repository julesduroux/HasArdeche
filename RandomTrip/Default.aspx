<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RandomTrip._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<div class="container">

      <div class="row row-offcanvas row-offcanvas-right">

        <div class="col-12 col-s-12">
          <div class="jumbotron">
			<div class="row">
			  <div class="col-3 col-lg-3">
				<img src="Content/TeamRandom.png" height="150" width="150" />
			  </div>
			  <div class="col-9 col-lg-9">
				<h1>Has'Ardèche</h1>
				<p>Sortez de votre bulle sans laisser de traces</p>
			  </div>
            </div>
		  </div>
				
          <div class="row" style="margin-bottom: 50px">
			<div class="row">
				<div class="col-3 col-lg-3">
				<asp:label runat="server" ID="labelHotel">Adresse de départ (ex : hôtel) :</asp:label>
				</div>
				<div class="col-3 col-lg-3">
					<asp:TextBox runat="server" ID="Adresse" Width="80%"></asp:TextBox>
				</div>
				<div class="col-2 col-lg-2">
					<asp:label runat="server" ID="labelDate">Date : </asp:label>
				</div>
				<div class="col-3 col-lg-3">
					<asp:TextBox runat="server" ID="Date" TextMode="Date" Width="80%"></asp:TextBox>
				</div>
			</div>
           <asp:Panel ID="listeElements" runat="server">

           </asp:Panel>
          </div>
			<div class="row">
		  <div align="center" class="col-6 col-lg-6">
			<asp:Button ID="search" height="100" width="50%" runat="server" OnClick="Page_Load" class="btn btn-primary" style="font-size: large;" Text="Make my day !"/>
		  </div>
		  <div align="center" class="col-6 col-lg-6">
			<asp:Button height="100" width="50%" ID="printButton" runat="server" class="btn btn-primary" Text="Print" OnClientClick="javascript:window.print();" />
		  </div>
		</div>
        </div>
      </div>

    </div>


</asp:Content>
