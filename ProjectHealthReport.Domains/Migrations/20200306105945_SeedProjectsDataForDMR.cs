using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class SeedProjectsDataForDMR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00T', 'Avensia 24/7', 'AMS 24/7', 'fredrik.spennare@niteco.se', '2019/03/07', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00Y', 'Maria Nila 24/7', 'AMS 24/7', 'fredrik.spennare@niteco.se', '2019/06/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00M', 'PAF maintenance', 'Baldur', 'fredrik.spennare@niteco.se', '2020/01/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00L', 'Vi i Villa', 'Baldur', 'fredrik.spennare@niteco.se', '2014/01/02', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00K', 'Maria Nila T&M', 'Baldur', 'fredrik.spennare@niteco.se', '2019/06/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('005', 'Ahum', 'HCMC', 'fredrik.spennare@niteco.se', '2019/10/17', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00A', 'Litium', 'HCMC', 'fredrik.spennare@niteco.se', '2018/01/02', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00B', 'Nymans', 'HCMC', 'fredrik.spennare@niteco.se', '2019/06/10', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00Q', 'Nobia MRT', 'Odin', 'fredrik.spennare@niteco.se', '2020/01/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01S', 'BN Intranet', 'Thor', 'fredrik.spennare@niteco.se', '2018/10/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01T', 'BNP - Management', 'Thor', 'fredrik.spennare@niteco.se', '2018/10/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01U', 'BNP BE and Borssnack', 'Thor', 'fredrik.spennare@niteco.se', '2018/10/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01V', 'BNP DnDi WordPress', 'Thor', 'fredrik.spennare@niteco.se', '2018/10/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01L', 'NIS-FH intergration', 'Thor', 'fredrik.spennare@niteco.se', '2016/11/07', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01M', 'Financial Hub', 'Thor', 'fredrik.spennare@niteco.se', '2018/10/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01Z', 'Service Plus', 'Thor', 'fredrik.spennare@niteco.se', '2018/10/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01O', 'CGI Driftsportal ', 'Thor', 'fredrik.spennare@niteco.se', '2019/09/26', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01P', 'CGI New Osloskolen Web Portal', 'Thor', 'fredrik.spennare@niteco.se', '2019/09/16', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01Q', 'CGI Oslo School Mobile App', 'Thor', 'fredrik.spennare@niteco.se', '2019/09/03', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01R', 'CGI Technical Account Manager', 'Thor', 'fredrik.spennare@niteco.se', '2019/08/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01X', 'Dialing.se', 'Thor', 'fredrik.spennare@niteco.se', '2019/11/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('021', 'Transvoice - Corporate Web Design', 'Thor', 'fredrik.spennare@niteco.se', '2019/06/03', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01K', 'Transvoice -  Public Site Development', 'Thor', 'fredrik.spennare@niteco.se', '2019/11/04', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('017', 'Transvoice - Maintenance', 'Thor', 'fredrik.spennare@niteco.se', '2019/10/07', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('013', 'Multisoft', 'Tyr', 'fredrik.spennare@niteco.se', '2019/03/18', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01C', 'Electrolux AEG Morocco Template Update', 'Frey', 'khanh.do@niteco.se', '2019/12/10', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01D', 'Electrolux South Africa', 'Frey', 'khanh.do@niteco.se', '2019/12/19', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01E', 'Electrolux APAC Website Improvement', 'Frey', 'khanh.do@niteco.se', '2019/12/23', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01F', 'Eluctrolux Isarel Website Launching and Content', 'Frey', 'khanh.do@niteco.se', '2019/12/26', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01G', 'Electrolux Arabia Website Launching and Content Entry', 'Frey', 'khanh.do@niteco.se', '2020/04/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01H', 'Electrolux Zanussi Isarel Website Launching and Content Entry', 'Frey', 'khanh.do@niteco.se', '2020/01/31', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01I', 'Electrolux AEG Isarel Website Launching and Content Entry', 'Frey', 'khanh.do@niteco.se', '2020/03/02', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('016', 'Electrolux AMAD APAC', 'Frey', 'khanh.do@niteco.se', '2019/07/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('018', 'Electrolux Marketing Automation', 'Marketing', 'khanh.do@niteco.se', '2019/01/21', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01A', 'Electrolux Content Publishing', 'Marketing', 'khanh.do@niteco.se', '2019/07/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01B', 'Electrolux GA-GTM', 'Marketing', 'khanh.do@niteco.se', '2019/07/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('020', 'Blue Grass - Postrack App', 'Thor', 'khanh.do@niteco.se', '2020/02/06', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00Z', 'Grant Thornton 24/7', 'AMS 24/7', 'mark.welland@niteco.se', '2019/12/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00X', 'Lexmod 24/7', 'AMS 24/7', 'mark.welland@niteco.se', '2019/10/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00U', 'Thinkmax 24/7', 'AMS 24/7', 'mark.welland@niteco.se', '2018/10/17', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00D', 'Delonghi T&M', 'Baldur', 'mark.welland@niteco.se', '2020/01/09', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00F', 'Delonghi content- Braun', 'Baldur', 'mark.welland@niteco.se', '2020/02/03', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00G', 'Delonghi content- Kenwood', 'Baldur', 'mark.welland@niteco.se', '2020/02/03', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00H', 'Delonghi content- SAP', 'Baldur', 'mark.welland@niteco.se', '2020/02/05', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00I', 'Delonghi content- Italy', 'Baldur', 'mark.welland@niteco.se', '2020/02/03', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('024', 'Grant Thornton', 'Baldur', 'mark.welland@niteco.se', '2019/12/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00J', 'High T&M', 'Baldur', 'mark.welland@niteco.se', '2017/07/14', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('023', 'Maginus MRT', 'Odin', 'mark.welland@niteco.se', '2019/12/22', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('026', 'Restockit - Design', 'Tyr', 'mark.welland@niteco.se', '2020/02/06', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00S', 'Altius 24/7', 'AMS 24/7', 'paul.tannock@niteco.se', '2018/12/26', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00V', 'Heineken 24/7', 'AMS 24/7', 'paul.tannock@niteco.se', '2019/11/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00W', 'MLA Monthly Report', 'AMS 24/7', 'paul.tannock@niteco.se', '2019/07/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('02C', 'Electrolux AMAD AU', 'Frey', 'paul.tannock@niteco.se', '2020/01/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('01J', 'Vintec Contractor Agreement', 'Frey', 'paul.tannock@niteco.se', '2019/12/30', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('002', 'Adairs BAU', 'HCMC', 'paul.tannock@niteco.se', '2020/02/03', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('003', 'Adairs AWS Beanstalk', 'HCMC', 'paul.tannock@niteco.se', '2020/02/03', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('004', 'Adairs First Class', 'HCMC', 'paul.tannock@niteco.se', '2020/01/02', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('006', 'Altius', 'HCMC', 'paul.tannock@niteco.se', '2019/09/09', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('007', 'Heineken MRT', 'HCMC', 'paul.tannock@niteco.se', '2019/09/02', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('008', 'MLA Monthly BAU', 'HCMC', 'paul.tannock@niteco.se', '2019/07/01', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('009', 'MLA Monthly BAU IT', 'HCMC', 'paul.tannock@niteco.se', '2019/10/21', 0, 2)
Insert into Projects (Code,Name, Division, KeyAccountManager, ProjectStartDate, PhrRequired, ProjectStateTypeId) Values('00C', 'Oxford Shop', 'HCMC', 'paul.tannock@niteco.se', '2019/01/11', 0, 2)

    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
