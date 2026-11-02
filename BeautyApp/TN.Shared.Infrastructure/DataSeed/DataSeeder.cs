using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using TN.Authentication.Domain.Entities;
using TN.Authentication.Domain.Static.Roles;
using TN.Setup.Domain.Entities;
using TN.Shared.Domain.IRepository;
using TN.Shared.Infrastructure.Data;

namespace TN.Shared.Infrastructure.DataSeed
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public DataSeeder(ApplicationDbContext applicationDbContext, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _context = applicationDbContext;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            
        }

        public async Task Seed()
        {
            await SeedProvince();
            await SeedDistrict();
            await SeedMunicipality();
            await SeedVdc();
            await SeedRole();
            await SeedModules();
            await SeedSubModules();
            await SeedMenus();

        }

        #region Roles

        private async Task SeedRole()
        {
            if(!await _roleManager.Roles.AnyAsync())
            {
                var roles = new List<IdentityRole>()
                {
                    new IdentityRole(){Name = Role.DeveloperUser},
                    new IdentityRole(){Name = Role.SuperAdmin},
                    new IdentityRole(){Name = Role.Admin}
                };

                foreach(var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }

        #endregion

        #region Province
        private async Task SeedProvince()
        {
            if(!await _context.Provinces.AnyAsync())
            {
                var province = new List<Province>()
                {
                    new Province(1,"कोशी","Koshi Province"),
                    new Province(2,"मधेश प्रदेश","Madhesh Province"),
                    new Province(3,"बाग्मती प्रदेश","Bagmati Province"),
                    new Province(4,"गण्डकी प्रदेश","Gandaki Province"),
                    new Province(5,"लुम्बिनी प्रदेश","Lumbini Province"),
                    new Province(6,"कर्णाली प्रदेश","Karnali Province"),
                    new Province(7,"सुदूरपश्चिम प्रदेश","Sudurpashchim Province"),
                };

                await _context.Provinces.AddRangeAsync(province);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion

        #region District
        private async Task SeedDistrict()
        {
            if(!await _context.Districts.AnyAsync())
            {
                var districts = new List<District>()
                {
                    new District (1,"ताप्लेजुङ","Taplejung",  1),
                      new District (2, "पाँचथर", "Panchthar",  1),
                      new District (3, "ईलाम", "Ilam",  1),
                      new District (4, "झापा", "Jhapa",  1),
                      new District (5, "मोरङ्ग", "Morang",  1),
                      new District (6, "सुनसरी", "Sunsari",  1),
                      new District (7, "धनकुटा", "Dhankuta",  1),
                      new District (8, "तेहथुम", "Terhathum",  1),
                      new District (9, "संखुवासभा", "Sankhuwasabha",  1),
                      new District (10, "भोजपुर", "Bhojpur", 1),
                      new District (11, "सोलुखुम्बु", "Solukhumbu",  1),
                      new District (12, "ओखलढुंगा", "Okhaldhunga",  1),
                      new District (13, "खोटाङ", "Khotang",  1),
                      new District (14, "उदयपुर", "Udayapur",  1),
                      new District (15, "सप्तरी", "Saptari",  2),
                      new District (16, "सिराहा", "Siraha",  2),
                      new District (17, "महोत्तरी", "Mahottari",  2),
                      new District (18, "सर्लाही", "Sarlahi",  2),
                      new District (19, "धनुषा", "Dhanusa",  2),
                      new District (20, "रौतहट", "Rautahat",  2),
                      new District (21, "बारा", "Bara",  2),
                      new District (22, "पर्सा", "Parsa",  2),
                      new District (23, "सिन्धुली", "Sindhuli",  3),
                      new District (24, "रामेछाप", "Ramechhap",  3),
                      new District (25, "दोलखा", "Dolakha",  3),
                      new District (26, "भक्तपुर", "Bhaktapur",  3),
                      new District (27, "सिन्धुपाल्चोक", "Sindhupalchok",  3),
                      new District (28, "काठमाडौँ", "Kathmandu",  3),
                      new District (29, "काभ्रेपलान्चोक", "Kavrepalanchok",  3),
                      new District (30, "ललितपुर", "Lalitpur",  3),
                      new District (31, "नुवाकोट", "Nuwakot",  3),
                      new District (32, "रसुवा", "Rasuwa",  3),
                      new District (33, "धादिङ", "Dhading",  3),
                      new District (34, "चितवन", "Chitwan",  3),
                      new District (35, "मकवानपुर", "Makwanpur",  3),
                      new District (36, "गोरखा", "Gorkha",  4),
                      new District (37, "लमजुङ", "Lamjung",  4),
                      new District (38, "कास्की", "Kaski",  4),
                      new District (39, "बागलुङ", "Baglung",  4),
                      new District (40, "मनाङ", "Manang",  4),
                      new District (41, "मुस्ताङ", "Mustang",  4),
                      new District (42, "म्याग्दी", "Myagdi",  4),
                      new District (43, "नवलपुर", "Nawalpur",  4),
                      new District (44, "पर्वत", "Parbat",  4),
                      new District (45, "स्याङग्जा", "Syangja",  4),
                      new District (46, "तनहुँ", "Tanahun",  4),
                      new District (47, "गुल्मी", "Gulmi",  5),
                      new District (48, "पाल्पा", "Palpa",  5),
                      new District (49, "रुपन्देही", "Rupandehi",  5),
                      new District (50, "कपिलवस्तु", "Kapilvastu",  5),
                      new District (51, "अर्घाखाँची", "Arghakhanchi",  5),
                      new District (52, "प्युठान", "Pyuthan",  5),
                      new District (53, "रोल्पा", "Rolpa",  5),
                      new District (54, "दाङ", "Dang",  5),
                      new District (55, "बाँके", "Banke",  5),
                      new District (56, "बर्दिया", "Bardiya",  5),
                      new District (57, "परासी", "Parasi",  5),
                      new District (58, "रूकुम(पूर्वी)", "Rukum(Eastern)",  5),
                      new District (59, "सल्यान", "Salyan",  6),
                      new District (60, "रूकुम(पश्चिमी)", "Rukum(Western)",  6),
                      new District (61, "डोल्पा", "Dolpa",  6),
                      new District (62, "हुम्ला", "Humla",  6),
                      new District (63, "जुम्ला", "Jumla",  6),
                      new District (64, "कालिकोट", "Kalikot",  6),
                      new District (65, "मुगु", "Mugu",  6),
                      new District (66, "सुर्खेत", "Surkhet",  6),
                      new District (67, "दैलेख", "Dailekh",  6),
                      new District (68, "जाजरकोट", "Jajarkot",  6),
                      new District (69, "कैलाली", "Kailali",  7),
                      new District (70, "अछाम", "Achham",  7),
                      new District (71, "डोटी", "Doti",  7),
                      new District (72, "बझाङ", "Bajhang",  7),
                      new District (73, "बाजुरा", "Bajura",  7),
                      new District (74, "कंचनपुर", "Kanchanpur",  7),
                      new District (75, "डडेलधुरा", "Dadeldhura",  7),
                      new District (76, "बैतडी", "Baitadi",  7),
                      new District (77, "दार्चुला", "Darchula",  7),

                };

                await _context.Districts.AddRangeAsync(districts); 
                await _unitOfWork.SaveChangesAsync();
            }

        }

        #endregion

        #region Municipality
        public async Task SeedMunicipality()
        {
            if(!await _context.Municipality.AnyAsync())
            {
                var municipalities = new List<Municipality>()
                {
                    new Municipality(1,"फुङलिङ नगरपालिका","Phungling Municipality", 1),

                     new Municipality(2,"फिदिम नगरपालिका","Phidim Municipality", 2),

                     new Municipality(3,"ईलाम नगरपालिका","Ilam Municipality", 3),

                     new Municipality(4,"देउमाई नगरपालिका","Deumai Municipality", 3),

                     new Municipality(5,"माई नगरपालिका","Mai Municipality", 3),

                     new Municipality(6,"सूर्योदय नगरपालिका","Suryodaya Municipality", 3),

                     new Municipality(7,"मेचीनगर नगरपालिका","MechiNagar Municipality", 4),

                     new Municipality(8,"दमक नगरपालिका","Damak  Municipality", 4),

                     new Municipality(9,"कन्काई नगरपालिका","Kankai  Municipality", 4),

                     new Municipality(10,"भद्रपुर नगरपालिका","Bhadrapur  Municipality", 4),

                     new Municipality(11,"अर्जुनधारा नगरपालिका","Arjundhara  Municipality", 4),

                     new Municipality(12,"शिवशताक्षी नगरपालिका","ShivaSataxi  Municipality", 4),

                     new Municipality(13,"गौरादह नगरपालिका","Gauradaha  Municipality", 4),

                     new Municipality(14,"विर्तामोड नगरपालिका","Birtamod  Municipality", 4),

                     new Municipality(15,"विराटनगर महानगरपालिका","Biratnagar  Metropolitan City", 5),

                     new Municipality(16,"बेलवारी नगरपालिका","Belbari Municipality", 5),

                     new Municipality(17,"लेटाङ नगरपालिका","Letang Municipality", 5),

                     new Municipality(18,"पथरी शनिश्चरे नगरपालिका","Pathari-Sanishchare  Municipality", 5),

                     new Municipality(19,"रंगेली नगरपालिका","Rangeli  Municipality", 5),

                     new Municipality(20,"रतुवामाई नगरपालिका","Ratuwamai  Municipality", 5),

                     new Municipality(21,"सुनवर्षि नगरपालिका","Sunbarshi  Municipality", 5),

                     new Municipality(22,"उर्लावारी नगरपालिका","Urlabari  Municipality", 5),

                     new Municipality(23,"सुन्दरहरैचा नगरपालिका","SundarHaraincha  Municipality", 5),

                     new Municipality(24,"ईटहरी उपमहानगरपालिका","Ithari Sub Metropolitan City", 6),

                     new Municipality(25,"धरान उपमहानगरपालिका","Dharan Sub Metropolitan City", 6),

                     new Municipality(26,"ईनरुवा नगरपालिका","Inaruwa  Municipality", 6),

                     new Municipality(27,"दुहवी नगरपालिका","Duhabi  Municipality", 6),

                     new Municipality(28,"रामधुनी नगरपालिका","Ramdhuni  Municipality", 6),

                     new Municipality(29,"बराहक्षेत्र नगरपालिका","Baraha  Municipality", 6),

                     new Municipality(30,"पाख्रिबास नगरपालिका","Pakhribas  Municipality", 7),

                     new Municipality(31,"धनकुटा नगरपालिका","Dhankuta  Municipality", 7),

                     new Municipality(32,"महालक्ष्मी नगरपालिका","Mahalaxmi  Municipality", 7),

                     new Municipality(33,"म्याङलुङ नगरपालिका","Myanglung  Municipality", 8),

                     new Municipality(34,"लालीगुराँस नगरपालिका","Laligurans  Municipality", 8),

                     new Municipality(35,"चैनपुर नगरपालिका","Chainpur  Municipality", 9),

                     new Municipality(36,"धर्मदेवी नगरपालिका","DharmaDevi  Municipality", 9),

                     new Municipality(37,"खाँदवारी नगरपालिका","Khadbari  Municipality", 9),

                     new Municipality(38,"मादी नगरपालिका","Madi  Municipality", 9),

                     new Municipality(39,"पाँचखपन नगरपालिका","PanchKhapan  Municipality", 9),

                     new Municipality(40,"भोजपुर नगरपालिका","Bhojpur  Municipality", 10),

                     new Municipality(41,"षडानन्द नगरपालिका","Shadananda  Municipality", 10),

                     new Municipality(42,"सोलुदुधकुण्ड नगरपालिका","SoluDudhakund  Municipality", 11),

                     new Municipality(43,"सिद्दिचरण नगरपालिका","SiddiCharan  Municipality", 12),

                     new Municipality(44,"हलेसी तुवाचुङ नगरपालिका","Halesi Tuwachung  Municipality", 13),

                     new Municipality(45,"दिक्तेल रुपाकोट मझुवागढी नगरपालिका","Diktel Rupakot Majhuwagadhi Municipality", 13),

                     new Municipality(46,"कटारी नगरपालिका","Katari  Municipality", 14),

                     new Municipality(47,"चौदण्डीगढी नगरपालिका","ChaudandiGadhi  Municipality", 14),

                     new Municipality(48,"त्रियुगा नगरपालिका","Triyuga  Municipality", 14),

                     new Municipality(49,"वेलका नगरपालिका","Belaka  Municipality", 14),

                     new Municipality(50,"राजविराज नगरपालिका","Rajbiraj  Municipality", 15),

                     new Municipality(51,"कञ्चनरुप नगरपालिका","Kanchanrup  Municipality", 15),

                     new Municipality(52,"डाक्नेश्वरी नगरपालिका","Dakneshwori  Municipality", 15),

                     new Municipality(53,"बोदेबरसाईन नगरपालिका","BodeBarsain  Municipality", 15),

                     new Municipality(54,"खडक नगरपालिका","Khadak  Municipality", 15),

                     new Municipality(55,"शम्भुनाथ नगरपालिका","Shambhunath  Municipality", 15),

                     new Municipality(56,"सुरुङ्‍गा नगरपालिका","Surunga  Municipality", 15),

                     new Municipality(57,"हनुमाननगर कङ्‌कालिनी नगरपालिका","HanumanNagar Kankalini Municipality", 15),

                     new Municipality(58,"सप्तकोशी नगरपालिका","Saptakoshi Municipality", 15),

                     new Municipality(59,"लहान नगरपालिका","Lahan  Municipality", 16),

                     new Municipality(60,"धनगढीमाई नगरपालिका","DhanagadhiMai  Municipality", 16),

                     new Municipality(61,"सिरहा नगरपालिका","Siraha  Municipality", 16),

                     new Municipality(62,"गोलबजार नगरपालिका","GolBazaar  Municipality", 16),

                     new Municipality(63,"मिर्चैयाँ नगरपालिका","Mirchaiya  Municipality", 16),

                     new Municipality(64,"कल्याणपुर नगरपालिका","Kalyanpur  Municipality", 16),

                     new Municipality(65,"कर्जन्हा नगरपालिका","Karjanha Municipality", 16),

                     new Municipality(66,"सुखीपुर नगरपालिका","Sukhipur Municipality", 16),

                     new Municipality(67,"जनकपुरधाम उपमहानगरपालिका","Janakpurdham Sub MetroCity", 19),

                     new Municipality(68,"क्षिरेश्वरनाथ नगरपालिका","Chhireshwarnath  Municipality", 19),

                     new Municipality(69,"गणेशमान चारनाथ नगरपालिका","Ganeshman Charnath  Municipality", 19),

                     new Municipality(70,"धनुषाधाम नगरपालिका","Dhanusadham  Municipality", 19),

                     new Municipality(71,"नगराइन नगरपालिका","Nagaraen  Municipality", 19),

                     new Municipality(72,"विदेह नगरपालिका","Bideh  Municipality", 19),

                     new Municipality(73,"मिथिला नगरपालिका","Mithila  Municipality", 19),

                     new Municipality(74,"शहीदनगर नगरपालिका","Shahid Nagar  Municipality", 19),

                     new Municipality(75,"सबैला नगरपालिका","Sabaila  Municipality", 19),

                     new Municipality(76,"कमला नगरपालिका","Kamala  Municipality", 19),

                     new Municipality(77,"मिथिला बिहारी नगरपालिका","Mithila Bihari Municipality", 19),

                     new Municipality(78,"हंसपुर नगरपालिका","Hanspur Municipality", 19),

                     new Municipality(79,"जलेश्वर नगरपालिका","Jaleshwor  Municipality", 17),

                     new Municipality(80,"बर्दिबास नगरपालिका","Bardibas  Municipality", 17),

                     new Municipality(81,"गौशाला नगरपालिका","Gaushala  Municipality", 17),

                     new Municipality(82,"लोहरपट्टी नगरपालिका","Loharpatti Municipality", 17),

                     new Municipality(83,"रामगोपालपुर नगरपालिका","RamGopalpur Municipality", 17),

                     new Municipality(84,"मनरा शिसवा नगरपालिका","Manara Shisawa Municipality", 17),

                     new Municipality(85,"मटिहानी नगरपालिका","Matihani Municipality", 17),

                     new Municipality(86,"भँगाहा नगरपालिका","Bhangaha Municipality", 17),

                     new Municipality(87,"बलवा नगरपालिका","Balawa Municipality", 17),

                     new Municipality(88,"औरही नगरपालिका","Aaurahi Municipality", 17),

                     new Municipality(89,"ईश्वरपुर नगरपालिका","Ishworpur  Municipality", 18),

                     new Municipality(90,"मलंगवा नगरपालिका","Malangawa  Municipality", 18),

                     new Municipality(91,"लालबन्दी नगरपालिका","Lalbandi  Municipality", 18),

                     new Municipality(92,"हरिपुर नगरपालिका","Haripur  Municipality", 18),

                     new Municipality(93,"हरिपुर्वा नगरपालिका","Haripurwa  Municipality", 18),

                     new Municipality(94,"हरिवन नगरपालिका","Hariwan  Municipality", 18),

                     new Municipality(95,"बरहथवा नगरपालिका","Barahathawa  Municipality", 18),

                     new Municipality(96,"बलरा नगरपालिका","Balara  Municipality", 18),

                     new Municipality(97,"गोडैटा नगरपालिका","Godaita  Municipality", 18),

                     new Municipality(98,"बागमती नगरपालिका","Bagamati  Municipality", 18),

                     new Municipality(99,"कविलासी नगरपालिका","Kabilasi Municipality", 18),

                     new Municipality(100,"चन्द्रपुर नगरपालिका","Chandrapur  Municipality", 20),


                     new Municipality(101,"गरुडा नगरपालिका","Garuda  Municipality", 20),

                     new Municipality(102,"गौर नगरपालिका","Gaur  Municipality", 20),

                     new Municipality(103,"बौधीमाई नगरपालिका","BoudhiMai Municipality", 20),

                     new Municipality(104,"बृन्दावन नगरपालिका","Brindaban Municipality", 20),

                     new Municipality(105,"देवाही गोनाही नगरपालिका","Devahi Gonahi Municipality", 20),

                     new Municipality(106,"गढीमाई नगरपालिका","GadhiMai Municipality", 20),

                     new Municipality(107,"गुजरा नगरपालिका","Gujara Municipality", 20),

                     new Municipality(108,"कटहरिया नगरपालिका","Katahariya Municipality", 20),

                     new Municipality(109,"माधव नारायण नगरपालिका","Madhav Narayan Municipality", 20),

                     new Municipality(110,"मौलापुर नगरपालिका","Moulapur Municipality", 20),

                     new Municipality(111,"फतुवाबिजयपुर नगरपालिका","Phatuwa Bijayapur Municipality", 20),

                     new Municipality(112,"ईशनाथ नगरपालिका","IshNath Municipality", 20),

                     new Municipality(113,"परोहा नगरपालिका","Paroha Municipality", 20),

                     new Municipality(114,"राजपुर नगरपालिका","Rajpur Municipality", 20),

                     new Municipality(115,"राजदेवी नगरपालिका","RajDevi Municipality", 20),

                     new Municipality(116,"कलैया उपमहानगरपालिका","Kalaiya Sub Metropolitan City", 21),

                     new Municipality(117,"जीतपुर सिमरा उपमहानगरपालिका","Jitpur Simara Sub Metropolitan City", 21),

                     new Municipality(118,"कोल्हवी नगरपालिका","Kolhabi  Municipality", 21),

                     new Municipality(119,"निजगढ नगरपालिका","Nijgadh  Municipality", 21),

                     new Municipality(120,"महागढीमाई नगरपालिका","Maha Gahdimai  Municipality", 21),

                     new Municipality(121,"सिम्रौनगढ नगरपालिका","Simraun Gadh  Municipality", 21),

                     new Municipality(122,"पचरौता नगरपालिका","PachaRouta Municipality", 21),

                     new Municipality(123,"बिरगंज महानगरपालिका","Birjung Metropolitan City", 22),

                     new Municipality(124,"पोखरिया नगरपालिका","Pokhariya  Municipality", 22),

                     new Municipality(125,"बहुदरमाई नगरपालिका","Bahudarmai Municipality", 22),

                     new Municipality(126,"पर्सागढी नगरपालिका","Parsagadhi Municipality", 22),

                     new Municipality(127,"कमलामाई नगरपालिका","Kamalamai  Municipality", 23),

                     new Municipality(128,"दुधौली नगरपालिका","Dudhauli  Municipality", 23),

                     new Municipality(129,"मन्थली नगरपालिका","Manthali  Municipality", 24),

                     new Municipality(130,"रामेछाप नगरपालिका","Ramechhap  Municipality", 24),

                     new Municipality(131,"जिरी नगरपालिका","Jiri  Municipality", 25),

                     new Municipality(132,"भिमेश्वर नगरपालिका","Bhimeshwor  Municipality", 25),

                     new Municipality(133,"चौतारा साँगाचोकगढी नगरपालिका","Chautara Sangachokgadhi Municipality", 27),

                     new Municipality(134,"बाह्रविसे नगरपालिका","Barhabise Municipality", 27),

                     new Municipality(135,"मेलम्ची नगरपालिका","Melamchi Municipality", 27),

                     new Municipality(136,"धुलिखेल नगरपालिका","Dhulikhel  Municipality", 29),

                     new Municipality(137,"बनेपा नगरपालिका","Baneps  Municipality", 29),

                     new Municipality(138,"पनौती नगरपालिका","Panauti  Municipality", 29),

                     new Municipality(139,"पाँचखाल नगरपालिका","Panchkhal Municipality", 29),

                     new Municipality(140,"नमोबुद्ध नगरपालिका","Namobuddha  Municipality", 29),

                     new Municipality(141,"मण्डनदेउपुर नगरपालिका"," Mandan Deupur Municipality", 29),

                     new Municipality(142,"ललितपुर महानगरपालिका","Lalitpur Metropolitan City", 30),

                     new Municipality(143,"गोदावरी नगरपालिका","Godawari Municipality", 30),

                     new Municipality(144,"महालक्ष्मी नगरपालिका","MahaLaxmi  Municipality", 30),

                     new Municipality(145,"चाँगुनारायण नगरपालिका","Changunarayan  Municipality", 26),

                     new Municipality(146,"भक्तपुर नगरपालिका","Bhaktapur  Municipality", 26),

                     new Municipality(147,"मध्यपुर थिमी नगरपालिका","Madhyapur Thimi Municipality", 26),

                     new Municipality(148,"सूर्यविनायक नगरपालिका","Surya Binayak  Municipality", 26),

                     new Municipality(149,"काठमाण्डौं महानगरपालिका","Kathmandu Metropolitan City", 28),

                     new Municipality(150,"कागेश्वरी मनोहरा नगरपालिका","Kageswori-Manohara  Municipality", 28),

                     new Municipality(151,"कीर्तिपुर नगरपालिका","Kirtipur  Municipality", 28),

                     new Municipality(152,"गोकर्णेश्वर नगरपालिका","Gokarneshwor  Municipality", 28),

                     new Municipality(153,"चन्द्रागिरी नगरपालिका","Chandragiri  Municipality", 28),

                     new Municipality(154,"टोखा नगरपालिका","Tokha  Municipality", 28),

                     new Municipality(155,"तारकेश्वर नगरपालिका","Tarkeshwor  Municipality", 28),

                     new Municipality(156,"दक्षिणकाली नगरपालिका","Daxinkali  Municipality", 28),

                     new Municipality(157,"नागार्जुन नगरपालिका","Nagarjun  Municipality", 28),

                     new Municipality(158,"बुढानिलकण्ठ नगरपालिका","Budhanialkantha  Municipality", 28),

                     new Municipality(159,"शङ्खरापुर नगरपालिका","Sankharapur  Municipality", 28),

                     new Municipality(160,"विदुर नगरपालिका","Bidur  Municipality", 31),

                     new Municipality(161,"बेलकोटगढी नगरपालिका","Belkot Gadhi  Municipality", 31),

                     new Municipality(162,"धुनीबेंशी नगरपालिका","Dhunibesi  Municipality", 33),

                     new Municipality(163,"निलकण्ठ नगरपालिका","Nilkantha  Municipality", 33),

                     new Municipality(164,"हेटौडा उपमहानगरपालिका","Hetauda Sub Metropolitan City", 35),

                     new Municipality(165,"थाहा नगरपालिका","Thaha Municipality", 35),

                     new Municipality(166,"भरतपुर महानगरपालिका","Bharatpur Metropolitan City", 34),

                     new Municipality(167,"कालिका नगरपालिका","Kalika  Municipality", 34),

                     new Municipality(168,"खैरहनी नगरपालिका","Khairhani  Municipality", 34),

                     new Municipality(169,"माडी नगरपालिका","Madi  Municipality", 34),

                     new Municipality(170,"रत्ननगर नगरपालिका","Ratna Nagar  Municipality", 34),

                     new Municipality(171,"राप्ती नगरपालिका","Rapti  Municipality", 34),

                     new Municipality(172,"गोरखा नगरपालिका","Gorkha  Municipality", 36),

                     new Municipality(173,"पालुङटार नगरपालिका","Palungtar  Municipality", 36),

                     new Municipality(174,"बेसीशहर नगरपालिका","Besishahar  Municipality", 37),

                     new Municipality(175,"मध्यनेपाल नगरपालिका","Madhya Nepal  Municipality", 37),

                     new Municipality(176,"रार्इनास नगरपालिका","Rainas  Municipality", 37),

                     new Municipality(177,"सुन्दरबजार नगरपालिका","Sundarbazar  Municipality", 37),

                     new Municipality(178,"भानु नगरपालिका","Bhanu  Municipality", 46),

                     new Municipality(179,"भिमाद नगरपालिका","Bhimad  Municipality", 46),

                     new Municipality(180,"व्यास नगरपालिका","Byas  Municipality", 46),

                     new Municipality(181,"शुक्लागण्डकी नगरपालिका","Sukla Gandaki  Municipality", 46),

                     new Municipality(182,"गल्याङ नगरपालिका","Galyang  Municipality", 45),

                     new Municipality(183,"चापाकोट नगरपालिका","Chapakot  Municipality", 45),

                     new Municipality(184,"पुतलीबजार नगरपालिका","Putalibazar  Municipality", 45),

                     new Municipality(185,"भीरकोट नगरपालिका","Bhirkot  Municipality", 45),

                     new Municipality(186,"वालिङ नगरपालिका","Waling  Municipality", 45),

                     new Municipality(187,"पोखरा महानगरपालिका","Pokhara  Metropolitan City", 38),

                     new Municipality(188,"बेनी नगरपालिका","Beni Municipality", 42),

                     new Municipality(189,"कुश्मा नगरपालिका","Kushma  Municipality", 44),

                     new Municipality(190,"फलेवास नगरपालिका","Phalebas  Municipality", 44),

                     new Municipality(191,"बागलुङ नगरपालिका","Baglung  Municipality", 39),

                     new Municipality(192,"गल्कोट नगरपालिका","Galkot  Municipality", 39),

                     new Municipality(193,"जैमूनी नगरपालिका","Jaimini  Municipality", 39),

                     new Municipality(194,"ढोरपाटन नगरपालिका","Dhorpatan  Municipality", 39),

                     new Municipality(195,"कावासोती नगरपालिका","Kawasoti  Municipality", 43),

                     new Municipality(196,"गैडाकोट नगरपालिका","Gaindakot  Municipality", 43),

                     new Municipality(197,"देवचुली नगरपालिका","Devchuli  Municipality", 43),

                     new Municipality(198,"मध्यविन्दु नगरपालिका","Madhya Bindu Municipality", 43),

                     new Municipality(199,"मुसिकोट नगरपालिका","Musikot  Municipality", 47),

                     new Municipality(200,"रेसुङ्गा नगरपालिका","Resunga  Municipality", 47),


                     new Municipality(201,"रामपुर नगरपालिका","Rampur  Municipality", 48),

                     new Municipality(202,"तानसेन नगरपालिका","Tansen  Municipality", 48),

                     new Municipality(203,"बुटवल उपमहानगरपालिका","Butwal Municipality", 49),

                     new Municipality(204,"देवदह नगरपालिका","Devdaha  Municipality", 49),

                     new Municipality(205,"लुम्बिनी सांस्कृतिक नगरपालिका","Lumbini Sanskritik  Municipality", 49),

                     new Municipality(206,"सैनामैना नगरपालिका","Siddharthanagar  Municipality", 49),

                     new Municipality(207,"सिद्धार्थनगर नगरपालिका","Saina Maina  Municipality", 49),

                     new Municipality(208,"तिलोत्तमा नगरपालिका","Tilottama  Municipality", 49),

                     new Municipality(209,"कपिलवस्तु नगरपालिका","Kapilbastu  Municipality", 50),

                     new Municipality(210,"बुद्धभुमी नगरपालिका","Buddabhumi  Municipality", 50),

                     new Municipality(211,"शिवराज नगरपालिका","Shivaraj  Municipality", 50),

                     new Municipality(212,"महाराजगंज नगरपालिका","Maharajganj  Municipality", 50),

                     new Municipality(213,"कृष्णनगर नगरपालिका","Krishna Nagar  Municipality", 50),

                     new Municipality(214,"बाणगंगा नगरपालिका","Banganga  Municipality", 50),

                     new Municipality(215,"सन्धिखर्क नगरपालिका","Sandhikharka  Municipality", 51),

                     new Municipality(216,"शितगंगा नगरपालिका","Shit Ganga  Municipality", 51),

                     new Municipality(217,"भूमिकास्थान नगरपालिका","Bhumikasthan  Municipality", 51),

                     new Municipality(218,"प्यूठान नगरपालिका","Pyuthan Municipality", 52),

                     new Municipality(219,"स्वर्गद्वारी नगरपालिका","Swargadwari  Municipality", 52),

                     new Municipality(220,"रोल्पा नगरपालिका","Rolpa Municipality", 53),

                     new Municipality(221,"तुल्सीपुर उपमहानगरपालिका","Tulsipur Sub Metropolitan City", 54),

                     new Municipality(222,"घोराही उपमहानगरपालिका","Ghorahi Sub Metropolitan City", 54),

                     new Municipality(223,"लमही नगरपालिका","Lamahi  Municipality", 54),

                     new Municipality(224,"नेपालगंज उपमहानगरपालिका","Nepalgunj Sub Metropolitan City", 55),

                     new Municipality(225,"कोहलपुर नगरपालिका","Kohalpur  Municipality", 55),

                     new Municipality(226,"गुलरिया नगरपालिका","Gulariya  Municipality", 56),

                     new Municipality(227,"मधुवन नगरपालिका","Maduvan  Municipality", 56),

                     new Municipality(228,"राजापुर नगरपालिका","Rajapur Municipality", 56),

                     new Municipality(229,"ठाकुरबाबा नगरपालिका","Thakura Baba  Municipality", 56),

                     new Municipality(230,"बाँसगढी नगरपालिका","Bansgadhi  Municipality", 56),

                     new Municipality(231,"बारबर्दिया नगरपालिका","Bar Bardiya  Municipality", 56),

                     new Municipality(232,"बर्दघाट नगरपालिका","Bardaghat Municipality", 57),

                     new Municipality(233,"रामग्राम नगरपालिका","Ramgram  Municipality", 57),

                     new Municipality(234,"सुनवल नगरपालिका","Sunwal  Municipality", 57),

                     new Municipality(235,"शारदा नगरपालिका","Sharada  Municipality", 59),

                     new Municipality(236,"बागचौर नगरपालिका","Bagchaur  Municipality", 59),

                     new Municipality(237,"बनगाड कुपिण्डे नगरपालिका","Bangad Kupinde  Municipality", 59),

                     new Municipality(238,"बीरेन्द्रनगर नगरपालिका","Birendra Nagar  Municipality", 66),

                     new Municipality(239,"भेरीगंगा नगरपालिका","Bheri Ganga  Municipality", 66),

                     new Municipality(240,"गुर्भाकोट नगरपालिका","Gurbhakot  Municipality", 66),

                     new Municipality(241,"पञ्चपुरी नगरपालिका","Panchapuri  Municipality", 66),

                     new Municipality(242,"लेकवेशी नगरपालिका","Lek Besi  Municipality", 66),

                     new Municipality(243,"नारायण नगरपालिका","Narayan  Municipality", 67),

                     new Municipality(244,"दुल्लु नगरपालिका","Dullu  Municipality", 67),

                     new Municipality(245,"चामुण्डा विन्द्रासैनी नगरपालिका","Chamunda Bindrasaini  Municipality", 67),

                     new Municipality(246,"आठबीस नगरपालिका","Aathabis  Municipality", 67),

                     new Municipality(247,"भेरी नगरपालिका","Bheri  Municipality", 68),

                     new Municipality(248,"छेडागाड नगरपालिका","Chhedagad  Municipality", 68),

                     new Municipality(249,"नलगाड नगरपालिका","Triveni Nalgad  Municipality", 68),

                     new Municipality(250,"ठूली भेरी नगरपालिका","Thuli Bheri  Municipality", 61),

                     new Municipality(251,"त्रिपुरासुन्दरी नगरपालिका","Tripura Sundari  Municipality", 61),

                     new Municipality(252,"चन्दननाथ नगरपालिका","Chandannath  Municipality", 63),

                     new Municipality(253,"खाँडाचक्र नगरपालिका","Khandachakra  Municipality", 64),

                     new Municipality(254,"रास्कोट नगरपालिका","Raskot  Municipality", 64),

                     new Municipality(255,"तिलागुफा नगरपालिका","Tila Gupha  Municipality", 64),

                     new Municipality(256,"छायाँनाथ रारा नगरपालिका","Chhaya Nath  Municipality", 65),

                     new Municipality(257,"मुसिकोट नगरपालिका","Musikot  Municipality", 60),

                     new Municipality(258,"चौरजहारी नगरपालिका","Chaurjahari  Municipality", 60),

                     new Municipality(259,"आठबिसकोट नगरपालिका","Aathabiskot  Municipality", 60),

                     new Municipality(260,"बडीमालिका नगरपालिका","Badi Malika  Municipality", 73),

                     new Municipality(261,"त्रिवेणी नगरपालिका","Triveni  Municipality", 73),

                     new Municipality(262,"बुढीगंगा नगरपालिका","Budhi Ganga  Municipality", 73),

                     new Municipality(263,"बुढीनन्दा नगरपालिका","Budhi Nanda  Municipality", 73),

                     new Municipality(264,"जयपृथ्वी नगरपालिका","Jaya Prithvi  Municipality", 72),

                     new Municipality(265,"बुंगल नगरपालिका","Bungal  Municipality", 72),

                     new Municipality(266,"मंगलसेन नगरपालिका","Mangalsen  Municipality", 70),

                     new Municipality(267,"कमलबजार नगरपालिका","Kamalbazar Municipality", 70),

                     new Municipality(268,"साँफेबगर नगरपालिका","Sanfebagar Municipality", 70),

                     new Municipality(269,"पन्चदेवल विनायक नगरपालिका","Panchdeval Binayak  Municipality", 70),

                     new Municipality(270,"दिपायल सिलगढी नगरपालिका","Dipayal-Silgadi  Municipality", 71),

                     new Municipality(271,"शिखर नगरपालिका","Shikhar  Municipality", 71),

                     new Municipality(272,"धनगढी उपमहानगरपालिका","Dhangadhi Sub Metropolitan City", 69),

                     new Municipality(273,"टिकापुर नगरपालिका","Tikapur  Municipality", 69),

                     new Municipality(274,"घोडाघोडी नगरपालिका","Ghodaghodi  Municipality", 69),

                     new Municipality(275,"लम्कीचुहा नगरपालिका","Lamki-Chuha  Municipality", 69),

                     new Municipality(276,"भजनी नगरपालिका","Bhajani  Municipality", 69),

                     new Municipality(277,"गोदावरी नगरपालिका","Godavari  Municipality", 69),

                     new Municipality(278,"गौरीगंगा नगरपालिका","Gauri Ganga  Municipality", 69),

                     new Municipality(279,"भीमदत्त नगरपालिका","Bhimdatta  Municipality", 74),

                     new Municipality(280,"पुर्नवास नगरपालिका","Punarbas  Municipality", 74),

                     new Municipality(281,"वेदकोट नगरपालिका","Bedkot  Municipality", 74),

                     new Municipality(282,"महाकाली नगरपालिका","Mahakali  Municipality", 74),

                     new Municipality(283,"शुक्लाफाँटा नगरपालिका","Shuklaphanta  Municipality", 74),

                     new Municipality(284,"बेलौरी नगरपालिका","Belauri  Municipality", 74),

                     new Municipality(285,"कृष्णपुर नगरपालिका","Krishnapur  Municipality", 74),

                     new Municipality(286,"अमरगढी नगरपालिका","Amargadhi  Municipality", 75),

                     new Municipality(287,"परशुराम नगरपालिका","Parashuram  Municipality", 75),

                     new Municipality(288,"दशरथचन्द नगरपालिका","Dasharath Chanda  Municipality", 76),

                     new Municipality(289,"पाटन नगरपालिका","Patan  Municipality", 76),

                     new Municipality(290,"मेलौली नगरपालिका","Melauli  Municipality", 76),

                     new Municipality(291,"पुर्चौडी नगरपालिका","Purchaundi  Municipality", 76),

                     new Municipality(292,"महाकाली नगरपालिका","Mahakali  Municipality", 77),

                     new Municipality(293,"शैल्यशिखर नगरपालिका","Sailya Shikhar  Municipality", 77),
                };

                await _context.Municipality.AddRangeAsync(municipalities);
                await _unitOfWork.SaveChangesAsync();

            }

        }

        #endregion

        #region Vdc
        public async Task SeedVdc()
        {
            if(!await _context.Vdcs.AnyAsync())
            {
             
                    var Vdcs = new List<Vdc>()
            {
                  new Vdc(1, "हतुवागढी गाउँपालिका", "Hatuwagadhi Rural Municipality", 10),

                  new Vdc(2, "रामप्रसाद राई गाउँपालिका", "Ramprasad Rai Rural Municipality", 10),

                  new Vdc(3, "आमचोक गाउँपालिका", "Aamchok Rural Municipality", 10),

                  new Vdc(4, "टेम्केमैयुङ गाउँपालिका", "Tyamkemaiyum Rural Municipality", 10),

                  new Vdc(5, "अरुण गाउँपालिका", "Arun Rural Municipality", 10),

                  new Vdc(6, "पौवादुङमा गाउँपालिका", "Pauwadungma Rural Municipality", 10),

                  new Vdc(7, "साल्पासिलिछो गाउँपालिका", "Salpa Silichho Rural Municipality", 10),

                  new Vdc(8, "सागुरीगढी गाउँपालिका", "Sangurigadhi Rural Municipality", 7),

                  new Vdc(9, "चौविसे गाउँपालिका", "Chaubise Rural Municipality", 7),

                  new Vdc(10, "सहिदभुमी गाउँपालिका", "Sahidbhumi Rural Municipality", 7),

                  new Vdc(11, "छथर जोरपाटी गाउँपालिका", "Chhathar Jorpati Rural Municipality", 7),

                  new Vdc(12, "फाकफोकथुम गाउँपालिका", "Phakphokthum Rural Municipality", 3),

                  new Vdc(13, "माईजोगमाई गाउँपालिका", "Mai Jogmai Rural Municipality", 3),

                  new Vdc(14, "चुलाचुली गाउँपालिका", "Chulachuli Rural Municipality", 3),

                  new Vdc(15, "रोङ गाउँपालिका", "Rong Rural Municipality", 3),

                  new Vdc(16, "माङसेबुङ गाउँपालिका", "Mangsebung Rural Municipality", 3),

                  new Vdc(17, "सन्दकपुर गाउँपालिका", "Sandakpur Rural Municipality", 3),

                  new Vdc(18, "कमल गाउँपालिका", "Kamal Rural Municipality", 4),

                  new Vdc(19, "बुद्धशान्ति गाउँपालिका", "Buddha Shanti Rural Municipality", 4),

                  new Vdc(20, "कचनकवल गाउँपालिका", "Kachankawal Rural Municipality", 4),

                  new Vdc(21, "झापा गाउँपालिका", "Jhapa Rural Municipality", 4),

                  new Vdc(22, "बाह्रदशी गाउँपालिका", "Barhadashi Rural Municipality", 4),

                  new Vdc(23, "गौरीगंज गाउँपालिका", "Gaurigunj Rural Municipality", 4),

                  new Vdc(24, "हल्दीवारी गाउँपालिका", "Haldibari Rural Municipality", 4),

                  new Vdc(25, "खोटेहाङ गाउँपालिका", "Khotehang Rural Municipality", 13),

                  new Vdc(26, "दिप्रुङ चुइचुम्मा गाउँपालिका", "Diprung Chuichumma Rural Municipality", 13),

                  new Vdc(27, "ऐसेलुखर्क गाउँपालिका", "Aiselukharka Rural Municipality", 13),

                  new Vdc(28, "जन्तेढुंगा गाउँपालिका", "Jantedhunga Rural Municipality", 13),

                  new Vdc(29, "केपिलासगढी गाउँपालिका", "Kepilasgadhi Rural Municipality", 13),

                  new Vdc(30, "बराहपोखरी गाउँपालिका", "Barahpokhari Rural Municipality", 13),

                  new Vdc(31, "रावा बेसी गाउँपालिका", "Rawa Besi Rural Municipality", 13),

                  new Vdc(32, "साकेला गाउँपालिका", "Sakela Rural Municipality", 13),

                  new Vdc(33, "जहदा गाउँपालिका", "Jahada Rural Municipality", 5),

                  new Vdc(34, "बुढीगंगा गाउँपालिका", "Budi Ganga Rural Municipality", 5),

                  new Vdc(35, "कटहरी गाउँपालिका", "Katahari Rural Municipality", 5),

                  new Vdc(36, "धनपालथान गाउँपालिका", "Dhanpalthan Rural Municipality", 5),

                  new Vdc(37, "कानेपोखरी गाउँपालिका", "Kanepokhari Rural Municipality", 5),

                  new Vdc(38, "ग्रामथान गाउँपालिका", "Gramthan Rural Municipality", 5),

                  new Vdc(39, "केरावारी गाउँपालिका", "Kerabari Rural Municipality", 5),

                  new Vdc(40, "मिक्लाजुङ गाउँपालिका", "Miklajung Rural Municipality", 5),

                  new Vdc(41, "मानेभञ्ज्याङ गाउँपालिका", "Mane Bhanjyang Rural Municipality", 12),

                  new Vdc(42, "चम्पादेवी गाउँपालिका", "Champadevi Rural Municipality", 12),

                  new Vdc(43, "सुनकोशी गाउँपालिका", "Sunkoshi Rural Municipality", 12),

                  new Vdc(44, "मोलुङ गाउँपालिका", "Molung Rural Municipality", 12),

                  new Vdc(45, "चिसंखुगढी गाउँपालिका", "Chisankhugadhi Rural Municipality", 12),

                  new Vdc(46, "खिजिदेम्बा गाउँपालिका", "Khiji Demba Rural Municipality", 12),

                  new Vdc(47, "लिखु गाउँपालिका", "Likhu Rural Municipality", 12),

                  new Vdc(48, "मिक्लाजुङ गाउँपालिका", "Miklajung Rural Municipality", 2),

                  new Vdc(49, "फाल्गुनन्द गाउँपालिका", "Phalgunanda Rural Municipality", 2),

                  new Vdc(50, "हिलिहाङ गाउँपालिका", "Hilihang Rural Municipality", 2),

                  new Vdc(51, "फालेलुङ गाउँपालिका", "Phalelung Rural Municipality", 2),

                  new Vdc(52, "याङवरक गाउँपालिका", "Yangbarak Rural Municipality", 2),

                  new Vdc(53, "कुम्मायक गाउँपालिका", "Kummayak Rural Municipality", 2),

                  new Vdc(54, "तुम्बेवा गाउँपालिका", "Tumbewa Rural Municipality", 2),

                  new Vdc(55, "मकालु गाउँपालिका", "Makalu Rural Municipality", 9),

                  new Vdc(56, "सिलीचोङ गाउँपालिका", "Silichong Rural Municipality", 9),

                  new Vdc(57, "सभापोखरी गाउँपालिका", "Sabhapokhari Rural Municipality", 9),

                  new Vdc(58, "चिचिला गाउँपालिका", "Chichila Rural Municipality", 9),

                  new Vdc(59, "भोटखोला गाउँपालिका", "Bhot Khola Rural Municipality", 9),

                  new Vdc(60, "थुलुङ दुधकोशी गाउँपालिका", "Thulung Dudhkoshi Rural Municipality", 11),

                  new Vdc(61, "नेचासल्यान गाउँपालिका", "Necha Salyan Rural Municipality", 11),

                  new Vdc(62, "माप्य दुधकोशी गाउँपालिका", "Mapya Dudhkoshi Rural Municipality", 11),

                  new Vdc(63, "महाकुलुङ गाउँपालिका", "Maha Kulung Rural Municipality", 11),

                  new Vdc(64, "सोताङ गाउँपालिका", "Sotang Rural Municipality", 11),

                  new Vdc(65, "खुम्बु पासाङल्हमु गाउँपालिका", "Khumbu Pasang Lhamu Rural Municipality", 11),

                  new Vdc(66, "लिखुपिके  गाउँपालिका", "Likhu Pike Rural Municipality", 11),

                  new Vdc(67, "कोशी गाउँपालिका", "Koshi Rural Municipality", 6),

                  new Vdc(68, "हरिनगर गाउँपालिका", "Harinagar Rural Municipality", 6),

                  new Vdc(69, "भोक्राहा गाउँपालिका", "Bhokraha Rural Municipality", 6),

                  new Vdc(70, "देवानगन्ज गाउँपालिका", "Dewangunj Rural Municipality", 6),

                  new Vdc(71, "गढी गाउँपालिका", "Gadhi Rural Municipality", 6),

                  new Vdc(72, "बर्जु गाउँपालिका", "Barju Rural Municipality", 6),

                  new Vdc(73, "सिरीजङ्घा गाउँपालिका", "Sirijangha Rural Municipality", 1),

                  new Vdc(74, "आठराई त्रिवेणी गाउँपालिका", "Aathrai Triveni Rural Municipality", 1),

                  new Vdc(75, "पाथिभरा याङवरक गाउँपालिका", "Pathibhara Yangbarak Rural Municipality", 1),

                  new Vdc(76, "मेरिङदेन गाउँपालिका", "Meringden Rural Municipality", 1),

                  new Vdc(77, "सिदिङ्वा गाउँपालिका", "Sidingwa Rural Municipality", 1),

                  new Vdc(78, "फक्ताङलुङ गाउँपालिका", "Phaktanglung Rural Municipality", 1),

                  new Vdc(79, "मैवाखोला गाउँपालिका", "Maiwa Khola Rural Municipality", 1),

                  new Vdc(80, "मिक्वाखोला गाउँपालिका", "Mikwa Khola Rural Municipality", 1),

                  new Vdc(81, "आठराई गाउँपालिका", "Aathrai Rural Municipality", 8),

                  new Vdc(82, "फेदाप गाउँपालिका", "Phedap Rural Municipality", 8),

                  new Vdc(83, "छथर गाउँपालिका", "Chhathar Rural Municipality", 8),

                  new Vdc(84, "मेन्छयायेम गाउँपालिका", "Menchayayem Rural Municipality", 8),

                  new Vdc(85, "उदयपुरगढी गाउँपालिका", "Udayapurgadhi Rural Municipality", 14),

                  new Vdc(86, "रौतामाई गाउँपालिका", "Rautamai Rural Municipality", 14),

                  new Vdc(87, "ताप्ली गाउँपालिका", "Tapli Rural Municipality", 14),

                  new Vdc(88, "लिम्चुङ्ग्बुङ गाउँपालिका", "Sunkoshi Rural Municipality", 14),

                  new Vdc(89, "सुवर्ण  गाउँपालिका", "Subarna Rural Municipality", 21),

                  new Vdc(90, "आदर्श कोतवाल गाउँपालिका", "Adarsha Kotwal Rural Municipality", 21),

                  new Vdc(91, "बारागढी गाउँपालिका", "Baragadhi Rural Municipality", 21),

                  new Vdc(92, "फेटा गाउँपालिका", "Pheta Rural Municipality", 21),

                  new Vdc(93, "करैयामाई गाउँपालिका", "Karaiyamai Rural Municipality", 21),

                  new Vdc(94, "प्रसौनी गाउँपालिका", "Prasauni Rural Municipality", 21),

                  new Vdc(95, "विश्रामपुर गाउँपालिका", "Bishrampur Rural Municipality", 21),

                  new Vdc(96, "देवताल गाउँपालिका", "Devtal Rural Municipality", 21),

                  new Vdc(97, "परवानीपुर गाउँपालिका", "Parawanipur Rural Municipality", 21),

                  new Vdc(98, "धनौजी   गाउँपालिका", "Mithila Bihari Rural Municipality", 19),

                  new Vdc(99, "औरही   गाउँपालिका", "Aaurahi Rural Municipality", 19),

                  new Vdc(100, "लक्ष्मीनिया  गाउँपालिका", "Laksminiya Rural Municipality", 19),


                  new Vdc(101, "मुखियापट्टी मुसहरमिया   गाउँपालिका", "Mukhiyapatti Musaharmiya Rural Municipality", 19),

                  new Vdc(102, "जनकनन्दिनी   गाउँपालिका", "Janak Nandini Rural Municipality", 19),

                  new Vdc(103, "बटेश्वर   गाउँपालिका", "Bateshwar Rural Municipality", 19),

                  new Vdc(104, "सोनमा गाउँपालिका", "Sonama Rural Municipality", 17),

                  new Vdc(105, "पिपरा गाउँपालिका", "Pipara Rural Municipality", 17),

                  new Vdc(106, "साम्सी गाउँपालिका", "Samsi Rural Municipality", 17),

                  new Vdc(107, "एकडारा गाउँपालिका", "Ekdara Rural Municipality", 17),

                  new Vdc(108, "महोत्तरी गाउँपालिका", "Mahottari Rural Municipality", 17),

                  new Vdc(109, "सखुवा प्रसौनी गाउँपालिका", "Sakhuwa Prasauni Rural Municipality", 22),

                  new Vdc(110, "जगरनाथपुर गाउँपालिका", "Jagarnathpur Rural Municipality", 22),

                  new Vdc(111, "छिपहरमाई गाउँपालिका", "Chhipaharmai Rural Municipality", 22),

                  new Vdc(112, "बिन्दबासिनी गाउँपालिका", "Bindabasini Rural Municipality", 22),

                  new Vdc(113, "पटेर्वा सुगौली गाउँपालिका", "Paterwa Sugauli Rural Municipality", 22),

                  new Vdc(114, "जिरा भवानी गाउँपालिका", "Jeera Bhavani Rural Municipality", 22),

                  new Vdc(115, "कालिकामाई गाउँपालिका", "Kalikamai Rural Municipality", 22),

                  new Vdc(116, "पकाहा मैनपुर गाउँपालिका", "Pakaha Mainpur Rural Municipality", 22),

                  new Vdc(117, "ठोरी गाउँपालिका", "Thori Rural Municipality", 22),

                  new Vdc(118, "धोबीनी गाउँपालिका", "Dhobini Rural Municipality", 22),

                  new Vdc(119, "दुर्गा भगवती गाउँपालिका", "Durga Bhagawati Rural Municipality", 20),

                  new Vdc(120, "यमुनामाई गाउँपालिका", "Yamunamai Rural Municipality", 20),

                  new Vdc(121, "तिलाठी कोईलाडी गाउँपालिका", "Tilathi Koiladi Rural Municipality", 15),

                  new Vdc(122, "बेल्ही चपेना गाउँपालिका", "Belhi Chapena Rural Municipality", 15),

                  new Vdc(123, "छिन्नमस्ता गाउँपालिका", "Chhinnamasta Rural Municipality", 15),

                  new Vdc(124, "महादेवा गाउँपालिका", "Mahadeva Rural Municipality", 15),

                  new Vdc(125, "अग्निसाइर कृष्णासवरन गाउँपालिका", "Aagnisaira Krishnasawaran Rural Municipality", 15),

                  new Vdc(126, "रुपनी गाउँपालिका", "Rupani Rural Municipality", 15),

                  new Vdc(143, "बलान-बिहुल गाउँपालिका", "Balan-Bihul Rural Municipality", 15),

                  new Vdc(144, "बिष्णुपुर गाउँपालिका", "Bishnupur Rural Municipality", 15),

                  new Vdc(152, "तिरहुत गाउँपालिका", "Tirhut Rural Municipality", 15),

                  new Vdc(153, "चक्रघट्टा  गाउँपालिका", "Chakraghatta Rural Municipality", 18),

                  new Vdc(154, "रामनगर   गाउँपालिका", "Ramnagar Rural Municipality", 18),

                  new Vdc(155, "विष्णु   गाउँपालिका", "Bishnu Rural Municipality", 18),

                  new Vdc(156, "ब्रह्मपुरी   गाउँपालिका", "Bramhapuri Rural Municipality", 18),

                  new Vdc(157, "चन्द्रनगर   गाउँपालिका", "Chandranagar Rural Municipality", 18),

                  new Vdc(158, "धनकौल   गाउँपालिका", "Dhankaul Rural Municipality", 18),

                  new Vdc(159, "कौडेना   गाउँपालिका", "Kaudena Rural Municipality", 18),

                  new Vdc(160, "पर्सा   गाउँपालिका", "Parsa Rural Municipality", 18),

                  new Vdc(165, "बसबरीया   गाउँपालिका", "Basbariya Rural Municipality", 18),

                  new Vdc(166, "लक्ष्मीपुर पतारी गाउँपालिका", "Laksmipur Patari Rural Municipality", 16),

                  new Vdc(167, "बरियारपट्टी गाउँपालिका", "Bariyarpatti Rural Municipality", 16),

                  new Vdc(168, "औरही गाउँपालिका", "Aaurahi Rural Municipality", 16),

                  new Vdc(169, "अर्नमा गाउँपालिका", "Arnama Rural Municipality", 16),

                  new Vdc(170, "भगवानपुर गाउँपालिका", "Bhagawanpur Rural Municipality", 16),

                  new Vdc(171, "नरहा गाउँपालिका", "Naraha Rural Municipality", 16),

                  new Vdc(172, "नवराजपुर गाउँपालिका", "Nawarajpur Rural Municipality", 16),

                  new Vdc(173, "सखुवानान्कारकट्टी गाउँपालिका", "Sakhuwanankarkatti Rural Municipality", 16),

                  new Vdc(174, "विष्णुपुर गाउँपालिका", "Bishnupur Rural Municipality", 16),

                  new Vdc(177, "इच्छाकामना गाउँपालिका", "Ichchhakamana Rural Municipality", 34),

                  new Vdc(178, "थाक्रे गाउँपालिका", "Thakre Rural Municipality", 33),

                  new Vdc(179, "बेनीघाट रोराङ्ग गाउँपालिका", "Benighat Rorang Rural Municipality", 33),

                  new Vdc(180, "गल्छी गाउँपालिका", "Galchhi Rural Municipality", 33),

                  new Vdc(181, "गजुरी गाउँपालिका", "Gajuri Rural Municipality", 33),

                  new Vdc(182, "ज्वालामूखी गाउँपालिका", "Jwalamukhi Rural Municipality", 33),

                  new Vdc(183, "सिद्धलेक गाउँपालिका", "Siddhalekh Rural Municipality", 33),

                  new Vdc(186, "त्रिपुरासुन्दरी गाउँपालिका", "Tripura Sundari Rural Municipality", 33),

                  new Vdc(187, "गङ्गाजमुना गाउँपालिका", "Gangajamuna Rural Municipality", 33),

                  new Vdc(188, "नेत्रावती डबजोङ गाउँपालिका", "Netrawati Dabjong Rural Municipality", 33),

                  new Vdc(189, "खनियाबास गाउँपालिका", "Khaniyabas Rural Municipality", 33),

                  new Vdc(190, "रुवी भ्याली गाउँपालिका", "Ruby Valley Rural Municipality", 33),

                  new Vdc(191, "कालिन्चोक गाउँपालिका", "Kalinchok Rural Municipality", 25),

                  new Vdc(194, "मेलुङ्ग गाउँपालिका", "Melung Rural Municipality", 25),

                  new Vdc(195, "शैलुङ्ग गाउँपालिका", "Shailung Rural Municipality", 25),

                  new Vdc(196, "वैतेश्वर गाउँपालिका", "Baiteshwar Rural Municipality", 25),

                  new Vdc(197, "तामाकोशी गाउँपालिका", "Tamakoshi Rural Municipality", 25),

                  new Vdc(198, "विगु गाउँपालिका", "Bigu Rural Municipality", 25),

                  new Vdc(199, "गौरीशङ्कर गाउँपालिका", "Gaurishankar Rural Municipality", 25),

                  new Vdc(200, "रोशी गाउँपालिका", "Roshi Rural Municipality", 29),

                  new Vdc(204, "तेमाल गाउँपालिका", "Temal Rural Municipality", 29),

                  new Vdc(205, "चौंरी देउराली गाउँपालिका", "Chaunri Deurali Rural Municipality", 29),

                  new Vdc(206, "भुम्लु गाउँपालिका", "Bhumlu Rural Municipality", 29),

                  new Vdc(207, "महाभारत गाउँपालिका", "Mahabharat Rural Municipality", 29),

                  new Vdc(208, "बेथानचोक गाउँपालिका", "Bethanchok Rural Municipality", 29),

                  new Vdc(209, "खानीखोला गाउँपालिका", "Khanikhola Rural Municipality", 29),

                  new Vdc(210, "बाग्मति गाउँपालिका", "Bagmati Rural Municipality", 30),

                  new Vdc(211, "कोन्ज्योसोम गाउँपालिका", "Konjyosom Rural Municipality", 30),

                  new Vdc(212, "महाङ्काल गाउँपालिका", "Mahankal Rural Municipality", 30),

                  new Vdc(219, "बकैया गाउँपालिका", "Bakaiya Rural Municipality", 35),

                  new Vdc(220, "मनहरी गाउँपालिका", "Manhari Rural Municipality", 35),

                  new Vdc(221, "बाग्मति गाउँपालिका", "Bagmati Rural Municipality", 35),

                  new Vdc(222, "राक्सिराङ्ग गाउँपालिका", "Raksirang Rural Municipality", 35),

                  new Vdc(223, "मकवानपुरगढी गाउँपालिका", "Makawanpurgadhi Rural Municipality", 35),

                  new Vdc(224, "कैलाश गाउँपालिका", "Kailash Rural Municipality", 35),

                  new Vdc(225, "भीमफेदी गाउँपालिका", "Bhimphedi Rural Municipality", 35),

                  new Vdc(229, "ईन्द्र सरोवर गाउँपालिका", "Indrasarowar Rural Municipality", 35),

                  new Vdc(230, "ककनी गाउँपालिका", "Kakani Rural Municipality", 31),

                  new Vdc(231, "दुप्चेश्वर गाउँपालिका", "Dupcheshwar Rural Municipality", 31),

                  new Vdc(232, "शिवपुरी गाउँपालिका", "Shivapuri Rural Municipality", 31),

                  new Vdc(233, "तादी गाउँपालिका", "Tadi Rural Municipality", 31),

                  new Vdc(234, "लिखु गाउँपालिका", "Likhu Rural Municipality", 31),

                  new Vdc(235, "सुर्यगढी गाउँपालिका", "Suryagadhi Rural Municipality", 31),

                  new Vdc(236, "पञ्चकन्या गाउँपालिका", "Panchakanya Rural Municipality", 31),

                  new Vdc(237, "तारकेश्वर गाउँपालिका", "Tarkeshwar Rural Municipality", 31),

                  new Vdc(238, "किस्पाङ गाउँपालिका", "Kispang Rural Municipality", 31),

                  new Vdc(239, "म्यागङ गाउँपालिका", "Myagang Rural Municipality", 31),

                  new Vdc(240, "खाँडादेवी गाउँपालिका", "Khandadevi Rural Municipality", 24),

                  new Vdc(241, "लिखु तामाकोशी गाउँपालिका", "Likhu Tamakoshi Rural Municipality", 24),

                  new Vdc(242, "दोरम्बा गाउँपालिका", "Doramba Rural Municipality", 24),

                  new Vdc(243, "गोकुलगङ्गा गाउँपालिका", "Gokulganga Rural Municipality", 24),

                  new Vdc(244, "सुनापती गाउँपालिका", "Sunapati Rural Municipality", 24),

                  new Vdc(245, "उमाकुण्ड गाउँपालिका", "Umakunda Rural Municipality", 24),


                  new Vdc(246, "नौकुण्ड गाउँपालिका", "Naukunda Rural Municipality", 32),

                  new Vdc(247, "कालिका गाउँपालिका", "Kalika Rural Municipality", 32),

                  new Vdc(248, "उत्तरगया गाउँपालिका", "Uttargaya Rural Municipality", 32),

                  new Vdc(249, "गोसाईकुण्ड गाउँपालिका", "Gosaikund Rural Municipality", 32),

                  new Vdc(250, "आमाछोदिङमो गाउँपालिका", "Aamachodingmo Rural Municipality", 32),

                  new Vdc(251, "तिनपाटन गाउँपालिका", "Tinpatan Rural Municipality", 23),

                  new Vdc(252, "मरिण गाउँपालिका", "Marin Rural Municipality", 23),

                  new Vdc(253, "हरिहरपुरगढी गाउँपालिका", "Hariharpurgadhi Rural Municipality", 23),

                  new Vdc(254, "सुनकोशी गाउँपालिका", "Sunkoshi Rural Municipality", 23),

                  new Vdc(255, "गोलन्जर गाउँपालिका", "Golanjor Rural Municipality", 23),

                  new Vdc(256, "फिक्कल गाउँपालिका", "Phikkal Rural Municipality", 23),

                  new Vdc(257, "घ्याङलेख गाउँपालिका", "Ghyanglekh Rural Municipality", 23),

                  new Vdc(260, "र्इन्द्रावती गाउँपालिका", "Indrawati Rural Municipality", 27),

                  new Vdc(261, "पाँचपोखरी थाङपाल गाउँपालिका", "Panchpokhari Thangpal Rural Municipality", 27),

                  new Vdc(262, "जुगल गाउँपालिका", "Jugal Rural Municipality", 27),

                  new Vdc(263, "बलेफी गाउँपालिका", "Balephi Rural Municipality", 27),

                  new Vdc(264, "हेलम्बु गाउँपालिका", "Helambu Rural Municipality", 27),

                  new Vdc(265, "भोटेकोशी गाउँपालिका", "Bhotekoshi Rural Municipality", 27),

                  new Vdc(266, "सुनकोशी गाउँपालिका", "Sunkoshi Rural Municipality", 27),

                  new Vdc(267, "लिसंखु पाखर गाउँपालिका", "Lisankhu Pakhar Rural Municipality", 27),

                  new Vdc(274, "त्रिपुरासुन्दरी गाउँपालिका", "Tripura Sundari Rural Municipality", 27),

                  new Vdc(277, "वडिगाड गाउँपालिका", "Badigad Rural Municipality", 39),

                  new Vdc(278, "काठेखोला गाउँपालिका", "Kathekhola Rural Municipality", 39),

                  new Vdc(279, "निसीखोला गाउँपालिका", "Nisikhola Rural Municipality", 39),

                  new Vdc(280, "बरेङ गाउँपालिका", "Bareng Rural Municipality", 39),

                  new Vdc(281, "ताराखोला गाउँपालिका", "Tarakhola Rural Municipality", 39),

                  new Vdc(282, "तमानखोला गाउँपालिका", "Tamankhola Rural Municipality", 39),

                  new Vdc(283, "शहिद लखन गाउँपालिका", "Shahid Lakhan Rural Municipality", 36),

                  new Vdc(284, "बारपाक सुलीकोट गाउँपालिका", "Barpak Sulikot Rural Municipality", 36),

                  new Vdc(285, "आरूघाट गाउँपालिका", "Aarughat Rural Municipality", 36),

                  new Vdc(290, "सिरानचोक गाउँपालिका", "Siranchok Rural Municipality", 36),

                  new Vdc(291, "गण्डकी गाउँपालिका", "Gandaki Rural Municipality", 36),

                  new Vdc(292, "भिमसेनथापा गाउँपालिका", "Bhimsen Thapa Rural Municipality", 36),

                  new Vdc(293, "अजिरकोट गाउँपालिका", "Ajirkot Rural Municipality", 36),

                  new Vdc(298, "धार्चे गाउँपालिका", "Dharche Rural Municipality", 36),

                  new Vdc(299, "चुम नुव्री गाउँपालिका", "Chum Nubri Rural Municipality", 36),

                  new Vdc(300, "अन्नपुर्ण गाउँपालिका", "Annapurna Rural Municipality", 38),

                  new Vdc(301, "माछापुछ्रे गाउँपालिका", "Machhapuchhre Rural Municipality", 38),

                  new Vdc(302, "मादी गाउँपालिका", "Madi Rural Municipality", 38),

                  new Vdc(303, "रूपा गाउँपालिका", "Rupa Rural Municipality", 38),

                  new Vdc(309, "मर्स्याङदी गाउँपालिका", "Marsyangdi Rural Municipality", 37),

                  new Vdc(310, "दोर्दी गाउँपालिका", "Dordi Rural Municipality", 37),

                  new Vdc(311, "दूधपोखरी गाउँपालिका", "Dudhpokhari Rural Municipality", 37),

                  new Vdc(312, "क्व्होलासोथार गाउँपालिका", "Kwaholasothar Rural Municipality", 37),

                  new Vdc(313, "मनाङ डिस्याङ गाउँपालिका", "Manang Disyang Rural Municipality", 40),

                  new Vdc(314, "नासोँ गाउँपालिका", "Nason Rural Municipality", 40),

                  new Vdc(316, "चामे गाउँपालिका", "Chame Rural Municipality", 40),

                  new Vdc(317, "नार्पा भूमि  गाउँपालिका", "Narpa Bhumi Rural Municipality", 40),

                  new Vdc(318, "घरपझोङ गाउँपालिका", "Gharpajhong Rural Municipality", 41),

                  new Vdc(319, "थासाङ गाउँपालिका", "Thasang Rural Municipality", 41),

                  new Vdc(320, "बारागुङ मुक्तिक्षेत्र गाउँपालिका", "Baragung Muktichhetra Rural Municipality", 41),

                  new Vdc(321, "लोमन्थाङ गाउँपालिका", "Lomanthang Rural Municipality", 41),

                  new Vdc(322, "लो-थेकर दामोदरकुण्ड गाउँपालिका", "Lo-Thekar Damodarkunda Rural Municipality", 41),

                  new Vdc(323, "मालिका गाउँपालिका", "Malika Rural Municipality", 42),

                  new Vdc(324, "मंगला गाउँपालिका", "Mangala Rural Municipality", 42),

                  new Vdc(325, "रघुगंगा गाउँपालिका", "Raghuganga Rural Municipality", 42),

                  new Vdc(326, "धवलागिरी गाउँपालिका", "Dhaulagiri Rural Municipality", 42),

                  new Vdc(327, "अन्नपुर्ण गाउँपालिका", "Annapurna Rural Municipality", 42),

                  new Vdc(328, "हुप्सेकोट गाउँपालिका", "Hupsekot Rural Municipality", 43),

                  new Vdc(330, "विनयी त्रिवेणी गाउँपालिका", "Binayi Triveni Rural Municipality", 43),

                  new Vdc(331, "बुलिङटार गाउँपालिका", "Bulingtar Rural Municipality", 43),

                  new Vdc(332, "बौदीकाली गाउँपालिका", "Baudikali Rural Municipality", 43),

                  new Vdc(333, "जलजला गाउँपालिका", "Jaljala Rural Municipality", 44),

                  new Vdc(334, "मोदी गाउँपालिका", "Modi Rural Municipality", 44),

                  new Vdc(337, "पैयूं गाउँपालिका", "Painyu Rural Municipality", 44),

                  new Vdc(338, "विहादी गाउँपालिका", "Bihadi Rural Municipality", 44),

                  new Vdc(339, "महाशिला गाउँपालिका", "Mahashila Rural Municipality", 44),

                  new Vdc(340, "कालीगण्डकी गाउँपालिका", "Kaligandaki Rural Municipality", 45),

                  new Vdc(341, "विरुवा गाउँपालिका", "Biruwa Rural Municipality", 45),

                  new Vdc(346, "हरिनास गाउँपालिका", "Harinas Rural Municipality", 45),

                  new Vdc(347, "आँधीखोला गाउँपालिका", "Aandhikhola Rural Municipality", 45),

                  new Vdc(348, "अर्जुन चौपारी गाउँपालिका", "Arjun Chaupari Rural Municipality", 45),

                  new Vdc(349, "फेदीखोला गाउँपालिका", "Phedikhola Rural Municipality", 45),

                  new Vdc(350, "ऋषिङ्ग गाउँपालिका", "Rishing Rural Municipality", 46),

                  new Vdc(351, "म्याग्दे गाउँपालिका", "Myagde Rural Municipality", 46),

                  new Vdc(356, "आँबुखैरेनी गाउँपालिका", "Aanbu Khaireni Rural Municipality", 46),

                  new Vdc(357, "बन्दिपुर गाउँपालिका", "Bandipur Rural Municipality", 46),

                  new Vdc(358, "घिरिङ गाउँपालिका", "Ghiring Rural Municipality", 46),

                  new Vdc(359, "देवघाट गाउँपालिका", "Devghat Rural Municipality", 46),

                  new Vdc(362, "मालारानी गाउँपालिका", "Malarani Rural Municipality", 51),

                  new Vdc(363, "पाणिनी गाउँपालिका", "Pandini Rural Municipality", 51),

                  new Vdc(364, "छत्रदेव गाउँपालिका", "Chhatradev Rural Municipality", 51),

                  new Vdc(365, "राप्ती सोनारी गाउँपालिका", "Raptisonari Rural Municipality", 55),

                  new Vdc(366, "वैजनाथ गाउँपालिका", "Baijnath Rural Municipality", 55),

                  new Vdc(367, "खजुरा गाउँपालिका", "Khajura Rural Municipality", 55),

                  new Vdc(368, "जानकी गाउँपालिका", "Janaki Rural Municipality", 55),

                  new Vdc(369, "डुडुवा गाउँपालिका", "Duduwa Rural Municipality", 55),

                  new Vdc(370, "नरैनापुर गाउँपालिका", "Narainapur Rural Municipality", 55),

                  new Vdc(371, "बढैयाताल गाउँपालिका", "Badhaiyatal Rural Municipality", 56),

                  new Vdc(374, "गेरुवा गाउँपालिका", "Geruwa Rural Municipality", 56),

                  new Vdc(375, "राप्ती गाउँपालिका", "Rapti Rural Municipality", 54),

                  new Vdc(376, "गढवा गाउँपालिका", "Gadhawa Rural Municipality", 54),

                  new Vdc(377, "बबई गाउँपालिका", "Babai Rural Municipality", 54),

                  new Vdc(378, "शान्तिनगर गाउँपालिका", "Shantinagar Rural Municipality", 54),

                  new Vdc(379, "राजपुर गाउँपालिका", "Rajpur Rural Municipality", 54),

                  new Vdc(380, "वंगलाचुली गाउँपालिका", "Banglachuli Rural Municipality", 54),

                  new Vdc(381, "दंगीशरण गाउँपालिका", "Dangisharan Rural Municipality", 54),

                  new Vdc(388, "सत्यवती गाउँपालिका", "Satyawati Rural Municipality", 47),

                  new Vdc(389, "धुर्कोट गाउँपालिका", "Dhurkot Rural Municipality", 47),

                  new Vdc(390, "गुल्मीदरवार गाउँपालिका", "Gulmi Durbar Rural Municipality", 47),


                  new Vdc(391, "मदाने गाउँपालिका", "Madane Rural Municipality", 47),

                  new Vdc(392, "चन्द्रकोट गाउँपालिका", "Chandrakot Rural Municipality", 47),

                  new Vdc(393, "मालिका गाउँपालिका", "Malika Rural Municipality", 47),

                  new Vdc(394, "छत्रकोट गाउँपालिका", "Chhatrakot Rural Municipality", 47),

                  new Vdc(395, "ईस्मा गाउँपालिका", "Isma Rural Municipality", 47),

                  new Vdc(396, "कालीगण्डकी गाउँपालिका", "Kaligandaki Rural Municipality", 47),

                  new Vdc(397, "रुरुक्षेत्र गाउँपालिका", "Rurukshetra Rural Municipality", 47),

                  new Vdc(404, "मायादेवी गाउँपालिका", "Mayadevi Rural Municipality", 50),

                  new Vdc(405, "शुद्धोधन गाउँपालिका", "Shuddhodhan Rural Municipality", 50),

                  new Vdc(406, "यसोधरा गाउँपालिका", "Yasodhara Rural Municipality", 50),

                  new Vdc(407, "विजयनगर गाउँपालिका", "Bijaynagar Rural Municipality", 50),

                  new Vdc(411, "सुस्ता गाउँपालिका", "Susta Rural Municipality", 57),

                  new Vdc(412, "प्रतापपुर गाउँपालिका", "Pratappur Rural Municipality", 57),

                  new Vdc(413, "सरावल गाउँपालिका", "Sarawal Rural Municipality", 57),

                  new Vdc(416, "पाल्हीनन्दन गाउँपालिका", "Palhi Nandan Rural Municipality", 57),

                  new Vdc(417, "रैनादेवी छहरा गाउँपालिका", "Rainadevi Chhahara Rural Municipality", 48),

                  new Vdc(418, "माथागढी गाउँपालिका", "Mathagadhi Rural Municipality", 48),

                  new Vdc(419, "निस्दी गाउँपालिका", "Nisdi Rural Municipality", 48),

                  new Vdc(420, "बगनासकाली  गाउँपालिका", "Bagnaskali Rural Municipality", 48),

                  new Vdc(421, "रम्भा गाउँपालिका", "Rambha Rural Municipality", 48),

                  new Vdc(422, "पूर्वखोला गाउँपालिका", "Purbakhola Rural Municipality", 48),

                  new Vdc(424, "तिनाउ गाउँपालिका", "Tinau Rural Municipality", 48),

                  new Vdc(425, "रिब्दीकोट गाउँपालिका", "Ribdikot Rural Municipality", 48),

                  new Vdc(426, "नौबहिनी गाउँपालिका", "Naubahini Rural Municipality", 52),

                  new Vdc(427, "झिमरुक गाउँपालिका", "Jhimaruk Rural Municipality", 52),

                  new Vdc(428, "गौमुखी गाउँपालिका", "Gaumukhi Rural Municipality", 52),

                  new Vdc(429, "ऐरावती गाउँपालिका", "Airawati Rural Municipality", 52),

                  new Vdc(430, "सरुमारानी गाउँपालिका", "Sarumarani Rural Municipality", 52),

                  new Vdc(431, "मल्लरानी गाउँपालिका", "Mallarani Rural Municipality", 52),

                  new Vdc(432, "माण्डवी गाउँपालिका", "Mandavi Rural Municipality", 52),

                  new Vdc(436, "सुनिल स्मृति गाउँपालिका", "Sunil Smriti Rural Municipality", 53),

                  new Vdc(437, "रुन्टीगढी गाउँपालिका", "Runtigadhi Rural Municipality", 53),

                  new Vdc(438, "लुङ्ग्री गाउँपालिका", "Lungri Rural Municipality", 53),

                  new Vdc(439, "त्रिवेणी गाउँपालिका", "Triveni Rural Municipality", 53),

                  new Vdc(440, "परिवर्तन गाउँपालिका", "Paribartan Rural Municipality", 53),

                  new Vdc(441, "गंगादेव गाउँपालिका", "Gangadev Rural Municipality", 53),

                  new Vdc(442, "माडी गाउँपालिका", "Madi Rural Municipality", 53),

                  new Vdc(445, "सुनछहरी गाउँपालिका", "Sunchhahari Rural Municipality", 53),

                  new Vdc(446, "थवाङ गाउँपालिका", "Thawang Rural Municipality", 53),

                  new Vdc(447, "भूमे गाउँपालिका", "Bhume Rural Municipality", 58),

                  new Vdc(448, "पुथा उत्तरगंगा गाउँपालिका", "Putha Uttarganga Rural Municipality", 58),

                  new Vdc(449, "सिस्ने गाउँपालिका", "Sisne Rural Municipality", 58),

                  new Vdc(450, "गैडहवा गाउँपालिका", "Gaidhawa Rural Municipality", 49),

                  new Vdc(457, "मायादेवी गाउँपालिका", "Mayadevi Rural Municipality", 49),

                  new Vdc(458, "कोटहीमाई गाउँपालिका", "Kotahimai Rural Municipality", 49),

                  new Vdc(462, "मर्चवारी गाउँपालिका", "Marchawari Rural Municipality", 49),

                  new Vdc(463, "सियारी गाउँपालिका", "Siyari Rural Municipality", 49),

                  new Vdc(464, "सम्मरीमाई गाउँपालिका", "Sammarimai Rural Municipality", 49),

                  new Vdc(465, "रोहिणी गाउँपालिका", "Rohini Rural Municipality", 49),

                  new Vdc(466, "शुद्धोधन गाउँपालिका", "Shuddhodhan Rural Municipality", 49),

                  new Vdc(467, "ओमसतीया गाउँपालिका", "Om Satiya Rural Municipality", 49),

                  new Vdc(468, "कञ्चन गाउँपालिका", "Kanchan Rural Municipality", 49),

                  new Vdc(472, "गुराँस गाउँपालिका", "Gurans Rural Municipality", 67),

                  new Vdc(473, "भैरवी गाउँपालिका", "Bhairabi Rural Municipality", 67),

                  new Vdc(474, "नौमुले गाउँपालिका", "Naumule Rural Municipality", 67),

                  new Vdc(475, "महावु गाउँपालिका", "Mahabu Rural Municipality", 67),

                  new Vdc(476, "ठाँटीकाँध गाउँपालिका", "Thantikandh Rural Municipality", 67),

                  new Vdc(477, "भगवतीमाई गाउँपालिका", "Bhagwatimai Rural Municipality", 67),

                  new Vdc(478, "डुंगेश्वर गाउँपालिका", "Dungeshwar Rural Municipality", 67),

                  new Vdc(484, "मुड्केचुला गाउँपालिका", "Mudkechula Rural Municipality", 61),

                  new Vdc(485, "काईके गाउँपालिका", "Kaike Rural Municipality", 61),

                  new Vdc(486, "शे फोक्सुन्डो गाउँपालिका", "She Phoksundo Rural Municipality", 61),

                  new Vdc(487, "जगदुल्ला गाउँपालिका", "Jagadulla Rural Municipality", 61),

                  new Vdc(492, "डोल्पो बुद्ध गाउँपालिका", "Dolpo Buddha Rural Municipality", 61),

                  new Vdc(493, "छार्का ताङसोङ गाउँपालिका", "Chharka Tongsong Rural Municipality", 61),

                  new Vdc(494, "सिमकोट गाउँपालिका", "Simkot Rural Municipality", 62),

                  new Vdc(495, "सर्केगाड गाउँपालिका", "Sarkegad Rural Municipality", 62),

                  new Vdc(496, "अदानचुली गाउँपालिका", "Adanchuli Rural Municipality", 62),

                  new Vdc(497, "खार्पुनाथ गाउँपालिका", "Kharpunath Rural Municipality", 62),

                  new Vdc(498, "ताँजाकोट गाउँपालिका", "Tanjakot Rural Municipality", 62),

                  new Vdc(502, "चंखेली गाउँपालिका", "Chankheli Rural Municipality", 62),

                  new Vdc(503, "नाम्खा गाउँपालिका", "Namkha Rural Municipality", 62),

                  new Vdc(504, "जुनीचाँदे गाउँपालिका", "Junichande Rural Municipality", 68),

                  new Vdc(505, "कुसे गाउँपालिका", "Kuse Rural Municipality", 68),

                  new Vdc(508, "बारेकोट गाउँपालिका", "Barekot Rural Municipality", 68),

                  new Vdc(509, "शिवालय गाउँपालिका", "Shivalaya Rural Municipality", 68),

                  new Vdc(510, "तातोपानी गाउँपालिका", "Tatopani Rural Municipality", 63),

                  new Vdc(511, "पातारासी गाउँपालिका", "Patarasi Rural Municipality", 63),

                  new Vdc(512, "तिला गाउँपालिका", "Tila Rural Municipality", 63),

                  new Vdc(513, "कनकासुन्दरी गाउँपालिका", "Kanaka Sundari Rural Municipality", 63),

                  new Vdc(515, "सिंजा गाउँपालिका", "Sinja Rural Municipality", 63),

                  new Vdc(516, "हिमा गाउँपालिका", "Hima Rural Municipality", 63),

                  new Vdc(517, "गुठिचौर गाउँपालिका", "Guthichaur Rural Municipality", 63),

                  new Vdc(518, "नरहरिनाथ गाउँपालिका", "Narharinath Rural Municipality", 64),

                  new Vdc(519, "पलाता गाउँपालिका", "Palata Rural Municipality", 64),

                  new Vdc(520, "शुभ कालिका गाउँपालिका", "Shubha Kalika Rural Municipality", 64),

                  new Vdc(521, "सान्नी त्रिवेणी गाउँपालिका", "Sanni Triveni Rural Municipality", 64),

                  new Vdc(525, "पचालझरना गाउँपालिका", "Pachaljharana Rural Municipality", 64),

                  new Vdc(526, "महावै गाउँपालिका", "Mahawai Rural Municipality", 64),

                  new Vdc(527, "खत्याड गाउँपालिका", "Khatyad Rural Municipality", 65),

                  new Vdc(528, "सोरु गाउँपालिका", "Soru Rural Municipality", 65),

                  new Vdc(529, "मुगुम कार्मारोंग गाउँपालिका", "Mugum Karmarong Rural Municipality", 65),

                  new Vdc(530, "सानीभेरी गाउँपालिका", "Sani Bheri Rural Municipality", 60),

                  new Vdc(532, "त्रिवेणी गाउँपालिका", "Triveni Rural Municipality", 60),

                  new Vdc(533, "बाँफिकोट गाउँपालिका", "Banphikot Rural Municipality", 60),

                  new Vdc(534, "कुमाख गाउँपालिका", "Kumakh Rural Municipality", 59),

                  new Vdc(535, "कालीमाटी गाउँपालिका", "Kalimati Rural Municipality", 59),

                  new Vdc(536, "छत्रेश्वरी गाउँपालिका", "Chhatreshwari Rural Municipality", 59),

                  new Vdc(537, "दार्मा गाउँपालिका", "Darma Rural Municipality", 59),

                  new Vdc(538, "कपुरकोट गाउँपालिका", "Kapurkot Rural Municipality", 59),


                  new Vdc(539, "त्रिवेणी गाउँपालिका", "Triveni Rural Municipality", 59),

                  new Vdc(540, "सिद्ध कुमाख गाउँपालिका", "Siddha Kumakh Rural Municipality", 59),

                  new Vdc(541, "बराहताल गाउँपालिका", "Barahatal Rural Municipality", 66),

                  new Vdc(545, "सिम्ता गाउँपालिका", "Simta Rural Municipality", 66),

                  new Vdc(546, "चौकुने गाउँपालिका", "Chaukune Rural Municipality", 66),

                  new Vdc(547, "चिङ्गाड गाउँपालिका", "Chingad Rural Municipality", 66),

                  new Vdc(552, "रामारोशन गाउँपालिका", "Ramaroshan Rural Municipality", 70),

                  new Vdc(553, "चौरपाटी गाउँपालिका", "Chaurpati Rural Municipality", 70),

                  new Vdc(554, "तुर्माखाँद गाउँपालिका", "Turmakhand Rural Municipality", 70),

                  new Vdc(555, "मेल्लेख गाउँपालिका", "Mellekh Rural Municipality", 70),

                  new Vdc(556, "ढँकारी गाउँपालिका", "Dhankari Rural Municipality", 70),

                  new Vdc(559, "बान्नीगडीजैगड गाउँपालिका", "Bannigadi Jayagad Rural Municipality", 70),

                  new Vdc(560, "दोगडाकेदार गाउँपालिका", "Dogdakedar Rural Municipality", 76),

                  new Vdc(561, "डिलाशैनी गाउँपालिका", "Dilashaini Rural Municipality", 76),

                  new Vdc(562, "सिगास गाउँपालिका", "Sigas Rural Municipality", 76),

                  new Vdc(563, "पञ्चेश्वर गाउँपालिका", "Pancheshwar Rural Municipality", 76),

                  new Vdc(564, "सुर्नया गाउँपालिका", "Surnaya Rural Municipality", 76),

                  new Vdc(565, "शिवनाथ गाउँपालिका", "Shivanath Rural Municipality", 76),

                  new Vdc(566, "केदारस्यु गाउँपालिका", "Kedarsyu Rural Municipality", 72),

                  new Vdc(567, "थलारा गाउँपालिका", "Thalara Rural Municipality", 72),

                  new Vdc(568, "बित्थडचिर गाउँपालिका", "Bitthadchir Rural Municipality", 72),

                  new Vdc(573, "छब्बीसपाथिभेरा गाउँपालिका", "Chhabis Pathibhera Rural Municipality", 72),

                  new Vdc(574, "छान्ना गाउँपालिका", "Chhanna Rural Municipality", 72),

                  new Vdc(575, "मष्टा गाउँपालिका", "Masta Rural Municipality", 72),

                  new Vdc(576, "दुर्गाथली गाउँपालिका", "Durgathali Rural Municipality", 72),

                  new Vdc(577, "तलकोट गाउँपालिका", "Talkot Rural Municipality", 72),

                  new Vdc(578, "सुर्मा गाउँपालिका", "Surma Rural Municipality", 72),

                  new Vdc(581, "सइपाल गाउँपालिका", "Saipal Rural Municipality", 72),

                  new Vdc(582, "खप्तड छेडेदह गाउँपालिका", "Khaptad Chhededaha Rural Municipality", 73),

                  new Vdc(583, "स्वामिकार्तिक खापर गाउँपालिका", "Swami Kartik Khapar Rural Municipality", 73),

                  new Vdc(584, "जगन्‍नाथ  गाउँपालिका", "Jagannath Rural Municipality", 73),

                  new Vdc(585, "हिमाली गाउँपालिका", "Himali Rural Municipality", 73),

                  new Vdc(586, "गौमुल गाउँपालिका", "Gaumul Rural Municipality", 73),

                  new Vdc(587, "नवदुर्गा गाउँपालिका", "Navadurga Rural Municipality", 75),

                  new Vdc(595, "आलिताल गाउँपालिका", "Aalitaal Rural Municipality", 75),

                  new Vdc(596, "गन्यापधुरा गाउँपालिका", "Ganyapadhura Rural Municipality", 75),

                  new Vdc(597, "भागेश्वर गाउँपालिका", "Bhageshwar Rural Municipality", 75),

                  new Vdc(598, "अजयमेरु गाउँपालिका", "Ajaymeru Rural Municipality", 75),

                  new Vdc(599, "नौगाड गाउँपालिका", "Naugad Rural Municipality", 77),

                  new Vdc(600, "मालिकार्जुन गाउँपालिका", "Malikarjun Rural Municipality", 77),

                  new Vdc(608, "मार्मा गाउँपालिका", "Marma Rural Municipality", 77),

                  new Vdc(609, "लेकम गाउँपालिका", "Lekam Rural Municipality", 77),

                  new Vdc(612, "दुहु गाउँपालिका", "Duhu Rural Municipality", 77),

                  new Vdc(613, "ब्यास गाउँपालिका", "Byas Rural Municipality", 77),

                  new Vdc(614, "अपि हिमाल गाउँपालिका", "Api Himal Rural Municipality", 77),

                  new Vdc(615, "आदर्श गाउँपालिका", "Aadarsha Rural Municipality", 71),

                  new Vdc(616, "पूर्वीचौकी गाउँपालिका", "Purbichauki Rural Municipality", 71),

                  new Vdc(621, "के.आई.सिं.  गाउँपालिका", "K.I. Singh Rural Municipality", 71),

                  new Vdc(622, "जोरायल गाउँपालिका", "Jorayal Rural Municipality", 71),

                  new Vdc(623, "सायल गाउँपालिका", "Sayal Rural Municipality", 71),

                  new Vdc(624, "बोगटान गाउँपालिका", "Bogatan Rural Municipality", 71),

                  new Vdc(625, "बड्डी केदार गाउँपालिका", "Badikedar Rural Municipality", 71),

                  new Vdc(626, "जानकी गाउँपालिका", "Janaki Rural Municipality", 69),

                  new Vdc(629, "कैलारी गाउँपालिका", "Kailari Rural Municipality", 69),

                  new Vdc(630, "जोशीपुर गाउँपालिका", "Joshipur Rural Municipality", 69),

                  new Vdc(631, "बर्गगोरिया गाउँपालिका", "Bargagoriya Rural Municipality", 69),

                  new Vdc(632, "मोहन्याल गाउँपालिका", "Mohanyal Rural Municipality", 69),

                  new Vdc(633, "चुरे गाउँपालिका", "Chure Rural Municipality", 69),

                  new Vdc(634, "लालझाँडी गाउँपालिका", "Laljhadi Rural Municipality", 74),

                  new Vdc(635, "बेलडाँडी गाउँपालिका", "Beldandi Rural Municipality", 74),
            };
                    await _context.Vdcs.AddRangeAsync(Vdcs);
                    await _unitOfWork.SaveChangesAsync();


                };

           
        }

        #endregion


        #region SeedModules
        private async Task SeedModules()
        {
            if (!await _context.Modules.AnyAsync())
            {
                var modules = new List<Modules>()
                {
                    new Modules("005b6875-807d-43c3-988e-f439f478c8b4","Inventory Management","1","-","/Inventory", true),
                    new Modules("8c834e6b-2510-48ee-b255-4994ffa49197","User Management","2","_","/User", true),
                    new Modules("95f57bfe-70d4-46f5-82ec-c287833824d8","Account Management","3","-","/Account", true),
                    new Modules("b62bcaca-b269-42d2-be33-aed2a9b79b0a","Role Management","4","-","/Role", true),
                };

                await _context.Modules.AddRangeAsync(modules);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion

        #region SeedSubModules
        private async Task SeedSubModules()
        {
            if (!await _context.SubModules.AnyAsync())
            {
                var subModules = new List<SubModules>()
                {
                    new SubModules("160cf130-a172-4005-bfe2-27479e2954d1","Conversion Factor","-","conversionFactor/all-ConversionFactor","005b6875-807d-43c3-988e-f439f478c8b4","1", true),
                    new SubModules("3f5fa4c7-beb3-4c81-8679-823ed2cd71b0","Ledger","-","ledger/all-ledger","95f57bfe-70d4-46f5-82ec-c287833824d8","2", true),
                    new SubModules("46fdf094-2c42-47e1-b9fe-1beab735ba82","Role Permission","-","role-permission/all-roles-permission","b62bcaca-b269-42d2-be33-aed2a9b79b0a","3", true),
                    new SubModules("52197ced-2cf3-45c9-a8e0-9c17942e44a3","Users","-","user/all-users","8c834e6b-2510-48ee-b255-4994ffa49197","3", true),
                    new SubModules("6506d801-f911-4f0c-919b-930ffd589a3d","Roles","-","role/all-Roles","b62bcaca-b269-42d2-be33-aed2a9b79b0a","4", true),
                    new SubModules("6902a024-e5d8-43d0-bdc0-eeee2e87afed","Items","-","item/all-Item","005b6875-807d-43c3-988e-f439f478c8b4","5", true),
                    new SubModules("cefa696b-0085-4b5f-88de-1579f6471a69","Journals","-","journal/all-journal","95f57bfe-70d4-46f5-82ec-c287833824d8","6", true),
                    new SubModules("d7b6ace6-8e62-43cb-b9c5-b92489fb796e","Units","-","units/all-Units","005b6875-807d-43c3-988e-f439f478c8b4","7", true),
                    new SubModules("d8a5be6b-3a83-4749-8cdf-26b5522970c3","Items Group","-","itemGroup/all-ItemGroup","005b6875-807d-43c3-988e-f439f478c8b4","8", true),

                };

                await _context.SubModules.AddRangeAsync(subModules);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion


        #region SeedMenus
        private async Task SeedMenus()
        {
            if (!await _context.Menus.AnyAsync())
            {
                var menus = new List<Menu>()
                {
                    new Menu("06426815-10d9-4617-8040-5da17b46e269","Delete","delete/itemsGroup","-","d8a5be6b-3a83-4749-8cdf-26b5522970c3",1, true),
                    new Menu("071b1e3a-d5a2-4053-8dfb-a436a2311435","Delete","delete/conversionFactor","-","160cf130-a172-4005-bfe2-27479e2954d1",1, true),
                    new Menu("0ce5fece-831f-40c8-827f-46e5a47a8f68","Add","add/rolePermission","-","46fdf094-2c42-47e1-b9fe-1beab735ba82",1, true),
                    new Menu("106105c6-dd4a-43b1-9a72-32085a3f1f1a","Edit","edit/journal","-","cefa696b-0085-4b5f-88de-1579f6471a69",1, true),
                    new Menu("1450dbe5-f304-493b-817d-f4f79968bdb0","Delete","delete/units","-","d7b6ace6-8e62-43cb-b9c5-b92489fb796e",1, true),
                    new Menu("17dc739a-5b3d-44bf-83a2-5f38dbeda851","Edit","edit/items","-","6902a024-e5d8-43d0-bdc0-eeee2e87afed",1, true),
                    new Menu("1ca49895-54a1-4590-9b4b-1af0b676adff","Edit","edit/ledger","-","3f5fa4c7-beb3-4c81-8679-823ed2cd71b0",1, true),
                    new Menu("225a047a-7a21-4fa3-82d9-4ef7f3db2459","Edit","edit/conversionFactor","-","160cf130-a172-4005-bfe2-27479e2954d1",1, true),
                    new Menu("33ea0146-4220-477d-ba3e-9febc43c4a4b","Add","add/ledger","-","3f5fa4c7-beb3-4c81-8679-823ed2cd71b0",1, true),
                    new Menu("38b5e29d-a3c8-4383-9b3f-6a79f5389377","Delete","User/Delete","-","52197ced-2cf3-45c9-a8e0-9c17942e44a3",1, true),
                    new Menu("3da68c38-6bc8-4f15-8d07-3274e57230de","Delete","delete/rolePermission","-","46fdf094-2c42-47e1-b9fe-1beab735ba82",1, true),
                    new Menu("428dfd7a-7541-4df2-bb40-8071c553428e","Assign","modulePermission/roles","-","6506d801-f911-4f0c-919b-930ffd589a3d",1, true),
                    new Menu("489bb34a-49c7-45d1-8fb3-2b95ce7f425a","Delete","delete/ledger","-","3f5fa4c7-beb3-4c81-8679-823ed2cd71b0",1, true),
                    new Menu("4afbd064-c66a-49cc-a5d7-b06037e314a4","Add","journal/add","-","cefa696b-0085-4b5f-88de-1579f6471a69",1, true),
                    new Menu("567a9575-b871-48c1-bd6e-b9e0a0b59091","Delete","delete/roles","-","6506d801-f911-4f0c-919b-930ffd589a3d",1, true),
                    new Menu("56d1ac2f-6d1c-47f8-a4b1-279d6fa227d1","Add","Add/User","-","52197ced-2cf3-45c9-a8e0-9c17942e44a3",1, true),
                    new Menu("583a1cfe-9e93-4efe-a5b2-2e343bc5c481","Edit","edit/itemsGroup","-","d8a5be6b-3a83-4749-8cdf-26b5522970c3",1, true),
                    new Menu("5bff0e61-7b16-40b5-842a-d6259107f37c","Edit","edit/roles","-","6506d801-f911-4f0c-919b-930ffd589a3d",1, true),
                    new Menu("5db70aee-af8f-4a74-90e1-28ae81a560ad","Edit","user/","-","52197ced-2cf3-45c9-a8e0-9c17942e44a3",1, true),
                    new Menu("66ac75c2-23c2-43d5-87ba-6023281d10e1","Add","add/conversionFactor","-","160cf130-a172-4005-bfe2-27479e2954d1",1, true),
                    new Menu("6ee9a440-4772-43e7-9c13-b34722e040b5","Delete","delete/items","-","6902a024-e5d8-43d0-bdc0-eeee2e87afed",1, true),
                    new Menu("7a9cd2b4-edc3-4c2e-9989-66180c7bb155","Assign","assignRoleToUser/rolePermission","-","46fdf094-2c42-47e1-b9fe-1beab735ba82",1, true),
                    new Menu("7daf643c-41c8-448b-b2bc-c898dfb030ad","Add","add/roles","-","6506d801-f911-4f0c-919b-930ffd589a3d",1, true),
                    new Menu("8ebfcadd-0222-414c-8b8c-c76df97a1bcf","Edit","edit/units","-","d7b6ace6-8e62-43cb-b9c5-b92489fb796e",1, true),
                    new Menu("b2fd5264-63b5-422b-9717-2041120128f9","Add","add/itemsGroup","-","d8a5be6b-3a83-4749-8cdf-26b5522970c3",1, true),
                    new Menu("c5383f8f-655d-47a9-a156-08b3a8bf2556","Assign","assignRole/User","-","52197ced-2cf3-45c9-a8e0-9c17942e44a3",1, true),
                    new Menu("cb1b124b-a0da-4bef-a2a3-a3c2f9fbb157","Delete","delete/journal","-","cefa696b-0085-4b5f-88de-1579f6471a69",1, true),
                    new Menu("e5544b94-0a75-4f14-bce6-99f443f43a40","Add","add/units","-","d7b6ace6-8e62-43cb-b9c5-b92489fb796e",1, true),
                    new Menu("f75b502d-d524-484b-bd80-4c3fe08941c3","Edit","edit/rolePermission","-","46fdf094-2c42-47e1-b9fe-1beab735ba82",1, true),
                    new Menu("fd7fd33a-4ace-409f-9725-690056f03e56","Add","add/items","-","6902a024-e5d8-43d0-bdc0-eeee2e87afed",1, true),
                 
                };

                await _context.Menus.AddRangeAsync(menus);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion





    }
}
