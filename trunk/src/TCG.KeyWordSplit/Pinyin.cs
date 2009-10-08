/* 
  * Copyright (C) 2009-2009 tcgcms.com <http://www.tcgcms.cn/> 
  *  
  *    本代码以公共的方式开发下载，任何个人和组织可以下载， 
  * 修改，进行第二次开发使用，但请保留作者版权信息。 
  *  
  *    任何个人或组织在使用本软件过程中造成的直接或间接损失， 
  * 需要自行承担后果与本软件开发者(三云鬼)无关。 
  *  
  *    本软件解决中小型商家产品网络化销售方案。 
  *     
  *    使用中的问题，咨询作者QQ邮箱 sanyungui@vip.qq.com 
  */

using System;
using System.Collections.Generic;
using System.Text;

using TCG.Entity;

namespace TCG.KeyWordSplit
{
    /// <summary>
    /// 拼音汉字对应
    /// </summary>
    public class Pinyin
    {
        private static List<Item> _chinesevspinyin = null;

        public static List<Item> ChineseVsPinyin
        {
            get
            {
                if (_chinesevspinyin == null)
                {
                    #region 添加值对
                    _chinesevspinyin = new List<Item>();
                    _chinesevspinyin.Add(new Item("a", "啊阿吖嗄腌锕"));
                    _chinesevspinyin.Add(new Item("ai", "埃挨哎唉哀皑癌蔼矮艾碍爱隘捱嗳嗌嫒瑷暧砹锿霭"));
                    _chinesevspinyin.Add(new Item("an", "鞍氨安俺按暗岸胺案谙埯揞犴庵桉铵鹌黯"));
                    _chinesevspinyin.Add(new Item("ang", "肮昂盎"));
                    _chinesevspinyin.Add(new Item("ao", "凹敖熬翱袄傲奥懊澳坳拗嗷岙廒遨媪骜獒聱螯鏊鳌鏖"));
                    _chinesevspinyin.Add(new Item("ba", "芭捌扒叭吧笆八疤巴拔跋靶把耙坝霸罢爸茇菝岜灞钯粑鲅魃"));
                    _chinesevspinyin.Add(new Item("bai", "白柏百摆佰败拜稗伯捭掰"));
                    _chinesevspinyin.Add(new Item("ban", "斑班搬扳般颁板版扮拌伴瓣半办绊阪坂贲钣瘢癍舨"));
                    _chinesevspinyin.Add(new Item("bang", "邦帮梆榜膀绑棒磅蚌镑傍谤蒡浜"));
                    _chinesevspinyin.Add(new Item("bao", "苞胞包褒剥薄雹保堡饱宝抱报暴豹鲍爆刨炮勹葆孢煲鸨褓趵龅"));
                    _chinesevspinyin.Add(new Item("bei", "杯碑悲卑北辈背贝钡倍狈备惫焙被臂孛陂邶埤萆蓓呗悖碚鹎褙鐾鞴"));
                    _chinesevspinyin.Add(new Item("ben", "奔苯本笨畚坌贲锛"));
                    _chinesevspinyin.Add(new Item("beng", "蚌崩绷甭泵蹦迸堋嘣甏"));
                    _chinesevspinyin.Add(new Item("bi", "逼鼻比鄙笔彼碧蓖蔽毕毙毖币庇痹闭敝弊必辟壁臂避陛秘泌匕俾埤芘荜荸萆薜吡哔狴庳愎滗濞弼妣婢嬖璧贲畀铋秕裨筚箅篦舭襞跸髀"));
                    _chinesevspinyin.Add(new Item("bian", "鞭边编贬扁便变卞辨辩辫遍匾弁苄忭汴缏煸砭碥窆褊蝙笾鳊"));
                    _chinesevspinyin.Add(new Item("biao", "标彪膘表婊骠杓飑飙飚灬镖镳瘭裱鳔髟"));
                    _chinesevspinyin.Add(new Item("bie", "鳖憋别瘪蹩"));
                    _chinesevspinyin.Add(new Item("bin", "彬斌濒滨宾摈傧豳缤玢槟殡膑镔髌鬓"));
                    _chinesevspinyin.Add(new Item("bing", "兵冰柄丙秉饼炳病并屏禀冫邴摒槟"));
                    _chinesevspinyin.Add(new Item("bo", "柏百剥薄玻菠播拨钵波博勃搏铂箔伯帛舶脖膊渤泊驳卜孛亳蕃啵饽檗擘礴钹鹁簸趵跛踣"));
                    _chinesevspinyin.Add(new Item("bu", "堡捕卜哺补埠不布步簿部怖埔卟逋瓿晡钚钸醭"));
                    _chinesevspinyin.Add(new Item("ca", "擦嚓礤"));
                    _chinesevspinyin.Add(new Item("cai", "猜裁材才财睬踩采彩菜蔡"));
                    _chinesevspinyin.Add(new Item("can", "餐参蚕残惭惨灿孱骖璨粲黪"));
                    _chinesevspinyin.Add(new Item("cang", "苍舱仓沧藏伧"));
                    _chinesevspinyin.Add(new Item("cao", "操糙槽曹草艹嘈漕螬艚"));
                    _chinesevspinyin.Add(new Item("ce", "厕策侧册测恻"));
                    _chinesevspinyin.Add(new Item("cen", "参岑涔"));
                    _chinesevspinyin.Add(new Item("ceng", "层蹭曾噌"));
                    _chinesevspinyin.Add(new Item("cha", "插叉茬茶查碴搽察岔差诧刹喳嚓猹馇汊姹杈楂槎檫锸镲衩"));
                    _chinesevspinyin.Add(new Item("chai", "差拆柴豺侪钗瘥虿龇"));
                    _chinesevspinyin.Add(new Item("chan", "搀掺蝉馋谗缠铲产阐颤单冁谄蒇廛忏潺澶孱羼婵觇禅镡蟾躔"));
                    _chinesevspinyin.Add(new Item("chang", "昌猖场尝常长偿肠厂敞畅唱倡伥鬯苌菖徜怅惝阊娼嫦昶氅鲳"));
                    _chinesevspinyin.Add(new Item("chao", "超抄钞朝嘲潮巢吵炒绰剿怊晁耖"));
                    _chinesevspinyin.Add(new Item("che", "车扯撤掣彻澈坼屮砗"));
                    _chinesevspinyin.Add(new Item("chen", "郴臣辰尘晨忱沉陈趁衬称谌谶抻嗔宸琛榇碜龀"));
                    _chinesevspinyin.Add(new Item("cheng", "撑称城橙成呈乘程惩澄诚承逞骋秤盛丞埕噌徵枨柽塍瞠铖铛裎蛏酲"));
                    _chinesevspinyin.Add(new Item("chi", "吃痴持匙池迟弛驰耻齿侈尺赤翅斥炽傺坻墀茌叱哧啻嗤彳饬媸敕眵鸱瘛褫蚩螭笞篪豉踟魑"));
                    _chinesevspinyin.Add(new Item("chong", "充冲虫崇宠种重茺忡憧铳舂艟"));
                    _chinesevspinyin.Add(new Item("chou", "抽酬畴踌稠愁筹仇绸瞅丑臭俦帱惆瘳雠"));
                    _chinesevspinyin.Add(new Item("chu", "初出橱厨躇锄雏滁除楚础储矗搐触处畜亍刍怵憷绌杵楮樗褚蜍蹰黜"));
                    _chinesevspinyin.Add(new Item("chuai", "揣搋嘬膪踹"));
                    _chinesevspinyin.Add(new Item("chuan", "川穿椽传船喘串舛遄巛氚钏舡"));
                    _chinesevspinyin.Add(new Item("chuang", "疮窗幢床闯创怆疒"));
                    _chinesevspinyin.Add(new Item("chui", "吹炊捶锤垂陲棰槌"));
                    _chinesevspinyin.Add(new Item("chun", "春椿醇唇淳纯蠢莼鹑蝽"));
                    _chinesevspinyin.Add(new Item("chuo", "戳绰啜辶辍踔龊"));
                    _chinesevspinyin.Add(new Item("ci", "差疵茨磁雌辞慈瓷词此刺赐次伺兹茈呲祠鹚粢糍"));
                    _chinesevspinyin.Add(new Item("cong", "聪葱囱匆从丛苁淙骢琮璁枞"));
                    _chinesevspinyin.Add(new Item("cou", "凑楱辏腠"));
                    _chinesevspinyin.Add(new Item("cu", "粗醋簇促蔟徂猝殂酢蹙蹴"));
                    _chinesevspinyin.Add(new Item("cuan", "蹿篡窜攒汆撺爨镩"));
                    _chinesevspinyin.Add(new Item("cui", "摧崔催脆瘁粹淬翠萃啐悴璀榱毳隹"));
                    _chinesevspinyin.Add(new Item("cun", "村存寸忖皴"));
                    _chinesevspinyin.Add(new Item("cuo", "磋撮搓措挫错厝嵯脞锉矬痤瘥鹾蹉躜"));
                    _chinesevspinyin.Add(new Item("da", "搭达答瘩打大耷哒嗒怛妲沓疸褡笪靼鞑"));
                    _chinesevspinyin.Add(new Item("dai", "大呆歹傣戴带殆代贷袋待逮怠埭甙呔岱迨骀绐玳黛"));
                    _chinesevspinyin.Add(new Item("dan", "耽担丹单郸掸胆旦氮但惮淡诞弹蛋赡石儋萏啖澹殚赕眈疸瘅聃箪"));
                    _chinesevspinyin.Add(new Item("dang", "当挡党荡档谠凼菪宕砀铛裆"));
                    _chinesevspinyin.Add(new Item("dao", "刀捣蹈倒岛祷导到稻悼道盗刂叨帱忉氘焘纛"));
                    _chinesevspinyin.Add(new Item("de", "德得的地锝"));
                    _chinesevspinyin.Add(new Item("dei", "得"));
                    _chinesevspinyin.Add(new Item("deng", "澄蹬灯登等瞪凳邓噔嶝戥磴镫簦"));
                    _chinesevspinyin.Add(new Item("di", "的堤低滴迪敌笛狄涤翟嫡抵底地蒂第帝弟递缔提氐籴诋谛邸坻荻嘀娣柢棣觌砥碲睇镝羝骶"));
                    _chinesevspinyin.Add(new Item("dia", "嗲"));
                    _chinesevspinyin.Add(new Item("dian", "颠掂滇碘点典靛垫电佃甸店惦奠淀殿阽坫巅玷钿癜癫簟踮"));
                    _chinesevspinyin.Add(new Item("diao", "碉叼雕凋刁掉吊钓调铞铫貂鲷"));
                    _chinesevspinyin.Add(new Item("die", "跌爹碟蝶迭谍叠垤堞揲喋牒瓞耋蹀鲽"));
                    _chinesevspinyin.Add(new Item("ding", "丁盯叮钉顶鼎锭定订仃啶玎腚碇町铤疔耵酊"));
                    _chinesevspinyin.Add(new Item("diu", "丢铥"));
                    _chinesevspinyin.Add(new Item("dong", "东冬董懂动栋侗恫冻洞咚岽峒氡胨胴硐鸫"));
                    _chinesevspinyin.Add(new Item("dou", "兜抖斗陡豆逗痘都蔸窦蚪篼"));
                    _chinesevspinyin.Add(new Item("du", "都督毒犊独读堵睹赌杜镀肚度渡妒芏嘟渎椟牍蠹笃髑黩"));
                    _chinesevspinyin.Add(new Item("duan", "端短锻段断缎椴煅簖"));
                    _chinesevspinyin.Add(new Item("dui", "堆兑队对怼憝碓"));
                    _chinesevspinyin.Add(new Item("dun", "墩吨蹲敦顿囤钝盾遁沌炖砘礅盹镦趸"));
                    _chinesevspinyin.Add(new Item("duo", "度掇哆多夺垛躲朵跺舵剁惰堕驮咄哚沲缍铎裰踱"));
                    _chinesevspinyin.Add(new Item("e", "阿蛾峨鹅俄额讹娥恶厄扼遏鄂饿哦噩谔垩苊莪萼呃愕屙婀轭腭锇锷鹗颚鳄"));
                    _chinesevspinyin.Add(new Item("ei", "诶"));
                    _chinesevspinyin.Add(new Item("en", "恩蒽摁"));
                    _chinesevspinyin.Add(new Item("er", "而儿耳尔饵洱二贰迩珥铒鸸鲕"));
                    _chinesevspinyin.Add(new Item("fa", "发罚筏伐乏阀法珐垡砝"));
                    _chinesevspinyin.Add(new Item("fan", "藩帆番翻樊矾钒繁凡烦反返范贩犯饭泛蕃蘩幡梵燔畈蹯"));
                    _chinesevspinyin.Add(new Item("fang", "坊芳方肪房防妨仿访纺放匚邡枋钫舫鲂"));
                    _chinesevspinyin.Add(new Item("fei", "菲非啡飞肥匪诽吠肺废沸费芾狒悱淝妃绯榧贲腓斐扉砩镄痱蜚篚翡霏鲱"));
                    _chinesevspinyin.Add(new Item("fen", "芬酚吩氛分纷坟焚汾粉奋份忿愤粪偾瀵玢棼贲鲼鼢"));
                    _chinesevspinyin.Add(new Item("feng", "丰封枫蜂峰锋风疯烽逢冯缝讽奉凤俸酆葑唪沣砜"));
                    _chinesevspinyin.Add(new Item("fo", "佛"));
                    _chinesevspinyin.Add(new Item("fou", "否缶"));
                    _chinesevspinyin.Add(new Item("fu", "佛夫敷肤孵扶拂辐幅氟符伏俘服浮涪福袱弗甫抚辅俯釜斧脯腑府腐赴副覆赋复傅付阜父腹负富讣附妇缚咐匐凫阝郛芙芾苻茯莩菔拊呋幞怫滏艴孚驸绂绋桴赙祓砩黻黼罘稃馥蚨蜉蝠蝮麸趺跗鲋鳆"));
                    _chinesevspinyin.Add(new Item("ga", "噶嘎夹咖伽尬尕尜旮钆"));
                    _chinesevspinyin.Add(new Item("gai", "该改概钙盖溉芥丐陔垓戤赅胲"));
                    _chinesevspinyin.Add(new Item("gan", "干甘杆柑竿肝赶感秆敢赣坩苷尴擀泔淦澉绀橄旰矸疳酐"));
                    _chinesevspinyin.Add(new Item("gang", "冈刚钢缸肛纲岗港杠扛戆罡筻"));
                    _chinesevspinyin.Add(new Item("gao", "篙皋高膏羔糕搞镐稿告睾诰郜藁缟槔槁杲锆"));
                    _chinesevspinyin.Add(new Item("ge", "盖哥歌搁戈鸽胳疙割革葛格蛤阁隔铬个各合咯鬲仡哿圪塥嗝纥搿膈铪镉袼虼舸骼"));
                    _chinesevspinyin.Add(new Item("gei", "给"));
                    _chinesevspinyin.Add(new Item("gen", "根跟亘茛哏艮"));
                    _chinesevspinyin.Add(new Item("geng", "耕更庚羹埂耿梗颈哽赓绠鲠"));
                    _chinesevspinyin.Add(new Item("gong", "工攻功恭龚供躬公宫弓巩汞拱贡共廾珙肱蚣觥"));
                    _chinesevspinyin.Add(new Item("gou", "钩勾沟苟狗垢构购够佝诟岣遘媾缑枸觏彀笱篝鞲"));
                    _chinesevspinyin.Add(new Item("gu", "辜菇咕箍估沽孤姑鼓古蛊骨谷股故顾固雇贾嘏诂菰崮汩梏轱牯牿臌毂瞽罟钴锢鸪鹄痼蛄酤觚鲴鹘"));
                    _chinesevspinyin.Add(new Item("gua", "刮瓜剐寡挂褂卦诖呱栝胍鸹"));
                    _chinesevspinyin.Add(new Item("guai", "乖拐怪掴"));
                    _chinesevspinyin.Add(new Item("guan", "棺关官冠观管馆罐惯灌贯纶倌莞掼涫盥鹳鳏"));
                    _chinesevspinyin.Add(new Item("guang", "光广逛咣犷桄胱"));
                    _chinesevspinyin.Add(new Item("gui", "瑰规圭硅归龟闺轨鬼诡癸桂柜跪贵刽炔匦刿庋宄妫桧炅晷皈簋鲑鳜"));
                    _chinesevspinyin.Add(new Item("gun", "辊滚棍衮绲磙鲧"));
                    _chinesevspinyin.Add(new Item("guo", "锅郭国果裹过涡馘埚掴呙帼崞猓椁虢锞聒蜾蝈"));
                    _chinesevspinyin.Add(new Item("ha", "蛤哈铪"));
                    _chinesevspinyin.Add(new Item("hai", "骸孩海氦亥害骇还咳嗨胲醢"));
                    _chinesevspinyin.Add(new Item("han", "酣憨邯韩含涵寒函喊罕翰撼捍旱憾悍焊汗汉邗菡撖瀚晗焓顸颔蚶鼾"));
                    _chinesevspinyin.Add(new Item("hang", "夯杭航吭巷行沆绗颃"));
                    _chinesevspinyin.Add(new Item("hao", "镐壕嚎豪毫郝好耗号浩貉蒿薅嗥嚆濠灏昊皓颢蚝"));
                    _chinesevspinyin.Add(new Item("he", "呵喝荷菏核禾和何合盒貉阂河涸赫褐鹤贺吓诃劾壑嗬阖纥曷盍颌蚵翮"));
                    _chinesevspinyin.Add(new Item("hei", "嘿黑"));
                    _chinesevspinyin.Add(new Item("hen", "痕很狠恨"));
                    _chinesevspinyin.Add(new Item("heng", "哼亨横衡恒蘅珩桁"));
                    _chinesevspinyin.Add(new Item("hong", "轰哄烘虹鸿洪宏弘红黉訇讧荭蕻薨闳泓"));
                    _chinesevspinyin.Add(new Item("hou", "喉侯猴吼厚候后堠後逅瘊篌糇鲎骺"));
                    _chinesevspinyin.Add(new Item("hu", "核呼乎忽瑚壶葫胡蝴狐糊湖弧虎唬护互沪户冱唿囫岵猢怙惚浒滹琥槲轷觳烀煳戽扈祜瓠鹄鹕鹱虍笏醐斛鹘"));
                    _chinesevspinyin.Add(new Item("hua", "花哗华猾滑画划化话骅桦砉铧"));
                    _chinesevspinyin.Add(new Item("huai", "槐徊怀淮坏踝"));
                    _chinesevspinyin.Add(new Item("huan", "欢环桓还缓换患唤痪豢焕涣宦幻郇奂垸萑擐圜獾洹浣漶寰逭缳锾鲩鬟"));
                    _chinesevspinyin.Add(new Item("huang", "荒慌黄磺蝗簧皇凰惶煌晃幌恍谎隍徨湟潢遑璜肓癀蟥篁鳇"));
                    _chinesevspinyin.Add(new Item("hui", "灰挥辉徽恢蛔回毁悔慧卉惠晦贿秽会烩汇讳诲绘溃诙茴荟蕙咴哕喙隳洄浍彗缋桧晖恚虺蟪麾"));
                    _chinesevspinyin.Add(new Item("hun", "荤昏婚魂浑混诨馄阍溷珲"));
                    _chinesevspinyin.Add(new Item("huo", "和豁活伙火获或惑霍货祸劐藿攉嚯夥钬锪镬耠蠖"));
                    _chinesevspinyin.Add(new Item("ji", "给击圾基机畸稽积箕肌饥迹激讥鸡姬绩缉吉极棘辑籍集及急疾汲即嫉级挤几脊己蓟技冀季伎祭剂悸济寄寂计记既忌际妓继纪藉奇系丌亟乩剞佶偈墼芨芰荠萁蒺蕺掎叽咭哜唧岌嵴洎彐屐骥畿玑楫殛戟戢赍觊犄齑矶羁嵇稷瘠虮笈笄暨跻跽霁鲚鲫髻麂"));
                    _chinesevspinyin.Add(new Item("jia", "嘉枷夹佳家加荚颊贾甲钾假稼价架驾嫁茄嘏伽郏葭岬浃迦珈戛胛恝铗铪镓痂瘕袷蛱笳袈跏"));
                    _chinesevspinyin.Add(new Item("jian", "歼监坚尖笺间煎兼肩艰奸缄茧检柬碱硷拣捡简俭剪减荐槛鉴践贱见键箭件健舰剑饯渐溅涧建僭谏谫谮菅蒹搛囝湔蹇謇缣枧楗戋戬牮犍毽腱睑锏鹣裥笕翦趼踺鲣鞯"));
                    _chinesevspinyin.Add(new Item("jiang", "虹僵姜将浆江疆蒋桨奖讲匠酱降强茳洚绛缰犟礓耩糨豇"));
                    _chinesevspinyin.Add(new Item("jiao", "蕉椒礁焦胶交郊浇骄娇嚼搅铰矫侥脚狡角饺缴绞剿教酵轿较叫窖觉校佼僬艽茭挢噍峤徼姣敫皎鹪蛟醮跤鲛"));
                    _chinesevspinyin.Add(new Item("jie", "揭接皆秸街阶截劫节桔杰捷睫竭洁结解姐戒藉芥界借介疥诫届偈讦诘卩拮喈嗟婕孑桀碣锴疖颉蚧羯鲒骱"));
                    _chinesevspinyin.Add(new Item("jin", "巾筋斤金今津襟紧锦仅谨进靳晋禁近烬浸尽劲卺荩堇噤馑廑妗缙瑾槿赆觐钅衿矜"));
                    _chinesevspinyin.Add(new Item("jing", "劲荆兢茎睛晶鲸京惊精粳经井警景颈静境敬镜径痉靖竟竞净刭儆阱陉菁獍憬泾迳弪婧肼胫腈旌靓"));
                    _chinesevspinyin.Add(new Item("jiong", "炯窘冂迥扃"));
                    _chinesevspinyin.Add(new Item("jiu", "揪究纠玖韭久灸九酒厩救旧臼舅咎就疚僦啾阄柩桕鸠鹫赳鬏"));
                    _chinesevspinyin.Add(new Item("ju", "车柜鞠拘狙疽居驹菊局咀矩举沮聚拒据巨具距踞锯俱句惧炬剧倨讵苣苴莒掬遽屦琚枸椐榘榉橘犋飓钜锔窭裾趄醵踽龃雎瞿鞫"));
                    _chinesevspinyin.Add(new Item("juan", "捐鹃娟倦眷卷绢圈鄄狷涓桊蠲锩镌隽"));
                    _chinesevspinyin.Add(new Item("jue", "嚼脚角撅攫抉掘倔爵觉决诀绝厥劂谲矍堀蕨噘崛獗孓珏桷橛爝镢蹶觖"));
                    _chinesevspinyin.Add(new Item("jun", "龟均菌钧军君峻俊竣浚郡骏捃皲筠麇"));
                    _chinesevspinyin.Add(new Item("ka", "喀咖卡咯佧咔胩"));
                    _chinesevspinyin.Add(new Item("kai", "开揩楷凯慨剀垲蒈忾恺铠锎锴"));
                    _chinesevspinyin.Add(new Item("kan", "槛刊堪勘坎砍看侃凵莰阚戡龛瞰"));
                    _chinesevspinyin.Add(new Item("kang", "康慷糠扛抗亢炕伉闶钪"));
                    _chinesevspinyin.Add(new Item("kao", "考拷烤靠尻栲犒铐"));
                    _chinesevspinyin.Add(new Item("ke", "呵坷苛柯棵磕颗科壳咳可渴克刻客课嗑岢恪溘骒缂珂轲氪瞌钶铪锞稞疴窠颏蚵蝌髁"));
                    _chinesevspinyin.Add(new Item("ken", "肯啃垦恳裉"));
                    _chinesevspinyin.Add(new Item("keng", "坑吭胫铒铿"));
                    _chinesevspinyin.Add(new Item("kong", "空恐孔控倥崆箜"));
                    _chinesevspinyin.Add(new Item("kou", "抠口扣寇芤蔻叩眍筘"));
                    _chinesevspinyin.Add(new Item("ku", "枯哭窟苦酷库裤刳堀喾绔骷"));
                    _chinesevspinyin.Add(new Item("kua", "夸垮挎跨胯侉锞"));
                    _chinesevspinyin.Add(new Item("kuai", "会块筷侩快蒯郐哙狯脍"));
                    _chinesevspinyin.Add(new Item("kuan", "宽款髋"));
                    _chinesevspinyin.Add(new Item("kuang", "匡筐狂框矿眶旷况诓诳邝圹夼哐纩贶"));
                    _chinesevspinyin.Add(new Item("kui", "亏盔岿窥葵奎魁傀馈愧溃馗匮夔隗蒉揆喹喟悝愦逵暌睽聩蝰篑跬"));
                    _chinesevspinyin.Add(new Item("kun", "坤昆捆困悃阃琨锟醌鲲髡"));
                    _chinesevspinyin.Add(new Item("kuo", "括扩廓阔蛞"));
                    _chinesevspinyin.Add(new Item("la", "垃拉喇蜡腊辣啦落剌邋旯砬瘌"));
                    _chinesevspinyin.Add(new Item("lai", "莱来赖崃徕涞濑赉睐铼癞籁"));
                    _chinesevspinyin.Add(new Item("lan", "蓝婪栏拦篮阑兰澜谰揽览懒缆烂滥岚漤榄斓罱镧褴"));
                    _chinesevspinyin.Add(new Item("lang", "琅榔狼廊郎朗浪莨蒗啷阆锒稂螂"));
                    _chinesevspinyin.Add(new Item("lao", "捞劳牢老佬姥酪烙涝落络唠崂栳铑铹痨耢醪"));
                    _chinesevspinyin.Add(new Item("le", "勒乐了仂叻泐鳓"));
                    _chinesevspinyin.Add(new Item("lei", "勒雷镭蕾磊累儡垒擂肋类泪羸诔嘞嫘缧檑耒酹"));
                    _chinesevspinyin.Add(new Item("leng", "棱楞冷塄愣"));
                    _chinesevspinyin.Add(new Item("li", "厘梨犁黎篱狸离漓理李里鲤礼莉荔吏栗丽厉励砾历利傈例俐痢立粒沥隶力璃哩鬲俪俚郦坜苈莅蓠藜呖唳喱猁溧澧逦娌嫠骊缡枥栎轹戾砺砬詈罹锂鹂疠疬蛎蜊蠡笠篥粝醴跞雳鲡鳢黧"));
                    _chinesevspinyin.Add(new Item("lia", "俩"));
                    _chinesevspinyin.Add(new Item("lian", "联莲连镰廉怜涟帘敛脸链恋炼练蔹奁潋濂琏楝殓臁裢裣蠊鲢"));
                    _chinesevspinyin.Add(new Item("liang", "俩粮凉梁粱良两辆量晾亮谅墚莨椋踉魉"));
                    _chinesevspinyin.Add(new Item("liao", "撩聊僚疗燎寥辽潦了撂镣廖料蓼尥嘹獠寮缭钌鹩"));
                    _chinesevspinyin.Add(new Item("lie", "列裂烈劣猎冽埒捩咧洌趔躐鬣"));
                    _chinesevspinyin.Add(new Item("lin", "琳林磷霖临邻鳞淋凛赁吝拎蔺啉嶙廪懔遴檩辚膦瞵粼躏麟"));
                    _chinesevspinyin.Add(new Item("ling", "棱玲菱零龄铃伶羚凌灵陵岭领另令酃苓呤囹泠绫柃棂瓴聆蛉翎鲮"));
                    _chinesevspinyin.Add(new Item("liu", "溜琉榴硫馏留刘瘤流柳六碌陆浏遛骝绺旒熘锍镏鹨鎏"));
                    _chinesevspinyin.Add(new Item("lo", "咯"));
                    _chinesevspinyin.Add(new Item("long", "龙聋咙笼窿隆垄拢陇弄垅茏泷珑栊胧砻癃"));
                    _chinesevspinyin.Add(new Item("lou", "楼娄搂篓漏陋露偻蒌喽嵝镂瘘耧蝼髅"));
                    _chinesevspinyin.Add(new Item("lu", "六芦卢颅庐炉掳卤虏鲁麓碌露路赂鹿潞禄录陆戮绿垆撸噜泸渌漉逯璐栌橹轳辂辘贲氇胪镥鸬鹭簏舻鲈"));
                    _chinesevspinyin.Add(new Item("lv", "驴吕铝侣旅履屡缕虑氯律率滤绿偻捋闾榈膂稆褛"));
                    _chinesevspinyin.Add(new Item("lve", "掠略锊"));
                    _chinesevspinyin.Add(new Item("luan", "峦挛孪滦卵乱脔娈栾鸾銮"));
                    _chinesevspinyin.Add(new Item("lun", "抡轮伦仑沦纶论囵"));
                    _chinesevspinyin.Add(new Item("luo", "铬咯烙萝螺罗逻锣箩骡裸落洛骆络倮蠃荦捋摞猡泺漯珞椤脶硌镙瘰雒"));
                    _chinesevspinyin.Add(new Item("m", "呒"));
                    _chinesevspinyin.Add(new Item("ma", "妈麻玛码蚂马骂嘛吗摩抹唛犸嬷杩蟆"));
                    _chinesevspinyin.Add(new Item("mai", "埋买麦卖迈脉劢荬霾"));
                    _chinesevspinyin.Add(new Item("man", "埋瞒馒蛮满蔓曼慢漫谩墁幔缦熳镘颟螨鳗鞔"));
                    _chinesevspinyin.Add(new Item("mang", "芒茫盲氓忙莽邙漭硭蟒"));
                    _chinesevspinyin.Add(new Item("mao", "猫茅锚毛矛铆卯茂冒帽貌贸袤茆峁泖瑁昴牦耄旄懋瞀蝥蟊髦"));
                    _chinesevspinyin.Add(new Item("me", "么"));
                    _chinesevspinyin.Add(new Item("mei", "玫枚梅酶霉煤没眉媒镁每美昧寐妹媚莓嵋猸浼湄楣镅鹛袂魅"));
                    _chinesevspinyin.Add(new Item("men", "门闷们扪焖懑钔"));
                    _chinesevspinyin.Add(new Item("meng", "萌蒙檬盟锰猛梦孟勐甍瞢懵朦礞虻蜢蠓艋艨"));
                    _chinesevspinyin.Add(new Item("mi", "眯醚靡糜迷谜弥米秘觅泌蜜密幂芈冖谧蘼咪嘧猕汨宓弭纟脒祢敉糸縻麋"));
                    _chinesevspinyin.Add(new Item("mian", "棉眠绵冕免勉娩缅面沔渑湎宀腼眄"));
                    _chinesevspinyin.Add(new Item("miao", "苗描瞄藐秒渺庙妙喵邈缈缪杪淼眇鹋"));
                    _chinesevspinyin.Add(new Item("mie", "蔑灭乜咩蠛篾"));
                    _chinesevspinyin.Add(new Item("min", "民抿皿敏悯闽苠岷闵泯缗玟珉愍黾鳘"));
                    _chinesevspinyin.Add(new Item("ming", "明螟鸣铭名命冥茗溟暝瞑酩"));
                    _chinesevspinyin.Add(new Item("miu", "谬缪"));
                    _chinesevspinyin.Add(new Item("mo", "脉没摸摹蘑模膜磨摩魔抹末莫墨默沫漠寞陌万谟茉蓦馍嫫殁镆秣瘼耱貊貘麽"));
                    _chinesevspinyin.Add(new Item("mou", "谋牟某侔哞缪眸蛑鍪"));
                    _chinesevspinyin.Add(new Item("mu", "模牟拇牡亩姆母墓暮幕募慕木目睦牧穆仫坶苜沐毪钼"));
                    _chinesevspinyin.Add(new Item("na", "拿哪呐钠那娜纳捺肭镎衲"));
                    _chinesevspinyin.Add(new Item("nai", "氖乃奶耐奈鼐佴艿萘柰"));
                    _chinesevspinyin.Add(new Item("nan", "南男难喃囡楠腩蝻赧"));
                    _chinesevspinyin.Add(new Item("nang", "囊攮囔馕曩"));
                    _chinesevspinyin.Add(new Item("nao", "挠脑恼闹淖孬垴呶猱瑙硇铙蛲"));
                    _chinesevspinyin.Add(new Item("ne", "哪呢讷"));
                    _chinesevspinyin.Add(new Item("nei", "馁内"));
                    _chinesevspinyin.Add(new Item("nen", "嫩恁"));
                    _chinesevspinyin.Add(new Item("neng", "能"));
                    _chinesevspinyin.Add(new Item("ng", "嗯"));
                    _chinesevspinyin.Add(new Item("ni", "呢妮霓倪泥尼拟你匿腻逆溺伲坭猊怩昵旎祢慝睨铌鲵"));
                    _chinesevspinyin.Add(new Item("nian", "蔫拈年碾撵捻念廿埝辇黏鲇鲶"));
                    _chinesevspinyin.Add(new Item("niang", "娘酿"));
                    _chinesevspinyin.Add(new Item("niao", "鸟尿茑嬲脲袅"));
                    _chinesevspinyin.Add(new Item("nie", "捏聂孽啮镊镍涅乜陧蘖嗫颞臬蹑"));
                    _chinesevspinyin.Add(new Item("nin", "您"));
                    _chinesevspinyin.Add(new Item("ning", "柠狞凝宁拧泞佞咛甯聍"));
                    _chinesevspinyin.Add(new Item("niu", "牛扭钮纽拗狃忸妞"));
                    _chinesevspinyin.Add(new Item("nong", "脓浓农弄侬哝"));
                    _chinesevspinyin.Add(new Item("nou", "耨"));
                    _chinesevspinyin.Add(new Item("nu", "奴努怒弩胬孥驽"));
                    _chinesevspinyin.Add(new Item("nv", "女恧钕衄"));
                    _chinesevspinyin.Add(new Item("nve", "虐疟"));
                    _chinesevspinyin.Add(new Item("nuan", "暖"));
                    _chinesevspinyin.Add(new Item("nuo", "娜挪懦糯诺傩搦喏锘"));
                    _chinesevspinyin.Add(new Item("o", "哦喔噢"));
                    _chinesevspinyin.Add(new Item("ou", "欧鸥殴藕呕偶沤区讴怄瓯耦"));
                    _chinesevspinyin.Add(new Item("pa", "扒耙啪趴爬帕怕琶葩杷筢"));
                    _chinesevspinyin.Add(new Item("pai", "拍排牌徘湃派迫俳蒎哌"));
                    _chinesevspinyin.Add(new Item("pan", "番攀潘盘磐盼畔判叛胖拚丬爿泮贲袢襻蟠蹒"));
                    _chinesevspinyin.Add(new Item("pang", "膀磅镑乓庞旁耪胖彷滂逄螃"));
                    _chinesevspinyin.Add(new Item("pao", "抛咆刨炮袍跑泡匏狍庖脬疱"));
                    _chinesevspinyin.Add(new Item("pei", "呸胚培裴赔陪配佩沛辔帔旆锫醅霈"));
                    _chinesevspinyin.Add(new Item("pen", "喷盆湓"));
                    _chinesevspinyin.Add(new Item("peng", "砰抨烹澎彭蓬棚硼篷膨朋鹏捧碰堋嘭怦蟛"));
                    _chinesevspinyin.Add(new Item("pi", "辟否坯砒霹批披劈琵毗啤脾疲皮匹痞僻屁譬丕仳陂陴邳郫圮埤鼙芘擗噼庀淠媲纰枇甓睥罴铍癖裨疋蚍蜱貔"));
                    _chinesevspinyin.Add(new Item("pian", "扁便篇偏片骗谝骈缏犏胼翩蹁"));
                    _chinesevspinyin.Add(new Item("piao", "飘漂瓢票朴剽嘌嫖骠缥殍瞟螵"));
                    _chinesevspinyin.Add(new Item("pie", "撇瞥丿苤氕"));
                    _chinesevspinyin.Add(new Item("pin", "拼频贫品聘姘嫔榀牝颦"));
                    _chinesevspinyin.Add(new Item("ping", "冯乒坪苹萍平凭瓶评屏俜娉枰鲆"));
                    _chinesevspinyin.Add(new Item("po", "泊繁坡泼颇婆破魄迫粕朴叵陂鄱珀攴攵钋钷皤笸"));
                    _chinesevspinyin.Add(new Item("pou", "剖裒掊"));
                    _chinesevspinyin.Add(new Item("pu", "堡暴脯扑铺仆莆葡菩蒲埔朴圃普浦谱曝瀑匍噗溥濮璞氆镤镨蹼"));
                    _chinesevspinyin.Add(new Item("qi", "稽缉期欺栖戚妻七凄漆柒沏其棋奇歧畦崎脐齐旗祈祁骑起岂乞企启契砌器气迄弃汽泣讫亟亓俟圻芑芪荠萋葺蕲嘁屺岐汔淇骐绮琪琦杞桤槭耆欹祺憩碛颀蛴蜞綦綮蹊鳍麒"));
                    _chinesevspinyin.Add(new Item("qia", "卡掐恰洽葜袷髂"));
                    _chinesevspinyin.Add(new Item("qian", "牵扦钎铅千迁签仟谦乾黔钱钳前潜遣浅谴堑嵌欠歉纤倩佥阡芊芡茜荨掮岍悭慊骞搴褰缱椠犍肷愆钤虔箝羟"));
                    _chinesevspinyin.Add(new Item("qiang", "枪呛腔羌墙蔷强抢丬戕嫱樯戗炝锵镪襁蜣羟跄"));
                    _chinesevspinyin.Add(new Item("qiao", "壳橇锹敲悄桥瞧乔侨巧鞘撬翘峭俏窍雀劁诮谯荞峤愀憔缲樵硗铫跷鞒"));
                    _chinesevspinyin.Add(new Item("qie", "切茄且怯窃伽郄惬慊妾挈锲箧趄"));
                    _chinesevspinyin.Add(new Item("qin", "钦侵亲秦琴勤芹擒禽寝沁芩揿吣嗪噙廑檎锓矜覃螓衾"));
                    _chinesevspinyin.Add(new Item("qing", "亲青轻氢倾卿清擎晴氰情顷请庆苘圊檠磬锖蜻罄箐綮謦鲭黥"));
                    _chinesevspinyin.Add(new Item("qiong", "琼穷邛茕穹蛩筇跫銎"));
                    _chinesevspinyin.Add(new Item("qiu", "仇龟秋丘邱球求囚酋泅俅巯犰湫逑遒楸赇虬蚯蝤裘糗鳅"));
                    _chinesevspinyin.Add(new Item("qu", "趋区蛆曲躯屈驱渠取娶龋趣去戌诎劬凵苣蕖蘧岖衢阒璩觑氍朐祛磲鸲癯蛐蠼麴瞿黢"));
                    _chinesevspinyin.Add(new Item("quan", "圈颧权醛泉全痊拳犬券劝诠荃犭悛绻辁畎铨蜷筌鬈"));
                    _chinesevspinyin.Add(new Item("que", "缺炔瘸却鹊榷确雀阕阙悫"));
                    _chinesevspinyin.Add(new Item("qui", "鼽"));
                    _chinesevspinyin.Add(new Item("qun", "裙群逡麇"));
                    _chinesevspinyin.Add(new Item("ran", "然燃冉染苒蚺髯"));
                    _chinesevspinyin.Add(new Item("rang", "瓤壤攘嚷让禳穰"));
                    _chinesevspinyin.Add(new Item("rao", "饶扰绕荛娆桡"));
                    _chinesevspinyin.Add(new Item("re", "惹热喏"));
                    _chinesevspinyin.Add(new Item("ren", "壬仁人忍韧任认刃妊纫亻仞荏葚饪轫稔衽"));
                    _chinesevspinyin.Add(new Item("reng", "扔仍"));
                    _chinesevspinyin.Add(new Item("ri", "日"));
                    _chinesevspinyin.Add(new Item("rong", "戎茸蓉荣融熔溶容绒冗嵘狨榕肜蝾"));
                    _chinesevspinyin.Add(new Item("rou", "揉柔肉糅蹂鞣"));
                    _chinesevspinyin.Add(new Item("ru", "茹蠕儒孺如辱乳汝入褥蓐薷嚅洳溽濡缛铷襦颥"));
                    _chinesevspinyin.Add(new Item("ruan", "软阮朊"));
                    _chinesevspinyin.Add(new Item("rui", "蕊瑞锐芮蕤枘睿蚋"));
                    _chinesevspinyin.Add(new Item("run", "闰润"));
                    _chinesevspinyin.Add(new Item("ruo", "若弱偌箬"));
                    _chinesevspinyin.Add(new Item("sa", "撒洒萨卅仨脎飒"));
                    _chinesevspinyin.Add(new Item("sai", "腮鳃塞赛噻"));
                    _chinesevspinyin.Add(new Item("san", "三叁伞散馓毵糁"));
                    _chinesevspinyin.Add(new Item("sang", "桑嗓丧搡磉颡"));
                    _chinesevspinyin.Add(new Item("sao", "搔骚扫嫂埽缫臊瘙鳋"));
                    _chinesevspinyin.Add(new Item("se", "塞瑟色涩啬铯穑"));
                    _chinesevspinyin.Add(new Item("sen", "森"));
                    _chinesevspinyin.Add(new Item("seng", "僧"));
                    _chinesevspinyin.Add(new Item("sha", "莎砂杀刹沙纱傻啥煞杉厦唼歃铩痧裟霎鲨"));
                    _chinesevspinyin.Add(new Item("shai", "色筛晒"));
                    _chinesevspinyin.Add(new Item("shan", "掺单珊苫杉山删煽衫闪陕擅赡膳善汕扇缮栅剡讪鄯埏芟彡潸姗嬗骟膻禅钐疝蟮舢跚鳝髟"));
                    _chinesevspinyin.Add(new Item("shang", "墒伤商赏晌上尚裳垧绱殇熵觞"));
                    _chinesevspinyin.Add(new Item("shao", "鞘梢捎稍烧芍勺韶少哨邵绍劭苕潲杓蛸筲艄"));
                    _chinesevspinyin.Add(new Item("she", "奢赊蛇舌舍赦摄射慑涉社设折厍佘揲猞滠歙畲铊麝"));
                    _chinesevspinyin.Add(new Item("shei", "谁"));
                    _chinesevspinyin.Add(new Item("shen", "参砷申呻伸身深娠绅神沈审婶甚肾慎渗什诜谂莘葚哂渖椹胂矧蜃"));
                    _chinesevspinyin.Add(new Item("sheng", "乘声生甥牲升绳省盛剩胜圣嵊渑晟眚笙"));
                    _chinesevspinyin.Add(new Item("shi", "匙师失狮施湿诗尸虱十石拾时什食蚀实识史矢使屎驶始式示士世柿事拭誓逝势是嗜噬适仕侍释饰氏市恃室视试似殖峙谥埘莳蓍弑饣轼贳炻礻铈铊螫舐筮酾豕鲥鲺"));
                    _chinesevspinyin.Add(new Item("shou", "收手首守寿授售受瘦兽扌狩绶艏"));
                    _chinesevspinyin.Add(new Item("shu", "蔬枢梳殊抒输叔舒淑疏书赎孰熟薯暑曙署蜀黍鼠属术述树束戍竖墅庶数漱恕丨倏塾菽摅沭澍姝纾毹腧殳镯秫疋"));
                    _chinesevspinyin.Add(new Item("shua", "刷耍唰"));
                    _chinesevspinyin.Add(new Item("shuai", "率摔衰甩帅蟀"));
                    _chinesevspinyin.Add(new Item("shuan", "栓拴闩涮"));
                    _chinesevspinyin.Add(new Item("shuang", "霜双爽泷孀"));
                    _chinesevspinyin.Add(new Item("shui", "水睡税说氵"));
                    _chinesevspinyin.Add(new Item("shun", "吮瞬顺舜"));
                    _chinesevspinyin.Add(new Item("shuo", "数说硕朔烁蒴搠妁槊铄"));
                    _chinesevspinyin.Add(new Item("si", "斯撕嘶思私司丝死肆寺嗣四伺似饲巳厮俟兕厶咝饣汜泗澌姒驷缌祀锶鸶耜蛳笥"));
                    _chinesevspinyin.Add(new Item("song", "松耸怂颂送宋讼诵凇菘崧嵩忪悚淞竦"));
                    _chinesevspinyin.Add(new Item("sou", "搜艘擞嗽叟薮嗖嗾馊溲飕瞍锼螋"));
                    _chinesevspinyin.Add(new Item("su", "苏酥俗素速粟僳塑溯宿诉肃缩夙谡蔌嗉愫涑簌觫稣"));
                    _chinesevspinyin.Add(new Item("suan", "酸蒜算狻"));
                    _chinesevspinyin.Add(new Item("sui", "尿虽隋随绥髓碎岁穗遂隧祟谇荽濉邃燧眭睢"));
                    _chinesevspinyin.Add(new Item("sun", "孙损笋荪狲飧榫隼"));
                    _chinesevspinyin.Add(new Item("suo", "莎蓑梭唆缩琐索锁所唢嗦嗍娑桫挲睃羧"));
                    _chinesevspinyin.Add(new Item("ta", "塌他它她塔獭挞蹋踏拓闼溻漯遢榻沓铊趿鳎"));
                    _chinesevspinyin.Add(new Item("tai", "胎苔抬台泰酞太态汰邰薹骀肽炱钛跆鲐"));
                    _chinesevspinyin.Add(new Item("tan", "弹坍摊贪瘫滩坛檀痰潭谭谈坦毯袒碳探叹炭郯澹昙赕忐钽锬镡覃"));
                    _chinesevspinyin.Add(new Item("tang", "汤塘搪堂棠膛唐糖倘躺淌趟烫傥帑饧溏瑭樘铛铴镗耥螗螳羰醣"));
                    _chinesevspinyin.Add(new Item("tao", "掏涛滔绦萄桃逃淘陶讨套鼗叨啕洮韬焘饕"));
                    _chinesevspinyin.Add(new Item("te", "特忒忑铽"));
                    _chinesevspinyin.Add(new Item("teng", "藤腾疼誊滕"));
                    _chinesevspinyin.Add(new Item("ti", "梯剔踢锑提题蹄啼体替嚏惕涕剃屉倜荑悌逖绨缇鹈裼醍"));
                    _chinesevspinyin.Add(new Item("tian", "天添填田甜恬舔腆掭忝阗殄畋钿锘"));
                    _chinesevspinyin.Add(new Item("tiao", "调挑条迢眺跳佻苕祧铫窕蜩笤粜龆鲦髫"));
                    _chinesevspinyin.Add(new Item("tie", "贴铁帖萜锇餮"));
                    _chinesevspinyin.Add(new Item("ting", "厅听烃汀廷停亭庭挺艇莛葶婷梃铤蜓霆"));
                    _chinesevspinyin.Add(new Item("tong", "通桐酮瞳同铜彤童桶捅筒统痛佟僮仝垌茼嗵峒恸潼砼"));
                    _chinesevspinyin.Add(new Item("tou", "偷投头透亠钭骰"));
                    _chinesevspinyin.Add(new Item("tu", "凸秃突图徒途涂屠土吐兔堍荼菟钍酴"));
                    _chinesevspinyin.Add(new Item("tuan", "湍团抟彖疃"));
                    _chinesevspinyin.Add(new Item("tui", "推颓腿蜕褪退煺"));
                    _chinesevspinyin.Add(new Item("tun", "囤褪吞屯臀氽饨暾豚"));
                    _chinesevspinyin.Add(new Item("tuo", "说拖托脱鸵陀驮驼椭妥拓唾乇佗坨庹沱柝柁橐砣铊箨酡跎鼍"));
                    _chinesevspinyin.Add(new Item("wa", "挖哇蛙洼娃瓦袜佤娲腽"));
                    _chinesevspinyin.Add(new Item("wai", "歪外崴"));
                    _chinesevspinyin.Add(new Item("wan", "蔓豌弯湾玩顽丸烷完碗挽晚皖惋宛婉万腕剜芄莞菀纨绾琬脘畹蜿鞔"));
                    _chinesevspinyin.Add(new Item("wang", "汪王亡枉网往旺望忘妄罔尢惘辋魍"));
                    _chinesevspinyin.Add(new Item("wei", "威巍微危韦违桅围唯惟为潍维苇萎委伟伪尾纬未蔚味畏胃喂魏位渭谓尉慰卫偎诿隈隗圩葳薇囗帏帷崴嵬猥猬闱沩洧涠逶娓玮韪軎炜煨痿艉鲔"));
                    _chinesevspinyin.Add(new Item("wen", "瘟温蚊文闻纹吻稳紊问刎阌汶玟璺雯"));
                    _chinesevspinyin.Add(new Item("weng", "嗡翁瓮蓊蕹"));
                    _chinesevspinyin.Add(new Item("wo", "挝蜗涡窝我斡卧握沃倭莴喔幄渥肟硪龌"));
                    _chinesevspinyin.Add(new Item("wu", "恶巫呜钨乌污诬屋无芜梧吾吴毋武五捂午舞伍侮坞戊雾晤物勿务悟误兀仵阢邬圬芴唔庑怃忤浯寤迕妩婺骛杌牾於焐鹉鹜痦蜈鋈鼯"));
                    _chinesevspinyin.Add(new Item("xi", "昔熙析西硒矽晰嘻吸锡牺稀息希悉膝夕惜熄烯溪汐犀檄袭席习媳喜铣洗系隙戏细匚僖兮隰郗茜菥葸蓰奚唏徙饩阋浠淅屣嬉玺樨曦觋欷歙熹禊禧皙穸裼蜥螅蟋舄舾羲粞翕醯蹊鼷"));
                    _chinesevspinyin.Add(new Item("xia", "瞎虾匣霞辖暇峡侠狭下厦夏吓呷狎遐瑕柙硖罅黠"));
                    _chinesevspinyin.Add(new Item("xian", "铣洗掀锨先仙鲜纤咸贤衔舷闲涎弦嫌显险现献县腺馅羡宪陷限线冼苋莶藓岘猃暹娴氙燹祆鹇痃痫蚬筅籼酰跣跹霰"));
                    _chinesevspinyin.Add(new Item("xiang", "降相厢镶香箱襄湘乡翔祥详想响享项巷橡像向象芗葙饷庠骧缃蟓鲞飨"));
                    _chinesevspinyin.Add(new Item("xiao", "萧硝霄削哮嚣销消宵淆晓小孝校肖啸笑效哓潇逍骁绡枭枵蛸筱箫魈"));
                    _chinesevspinyin.Add(new Item("xie", "解楔些歇蝎鞋协挟携邪斜胁谐写械卸蟹懈泄泻谢屑血叶偕亵勰燮薤撷獬廨渫瀣邂绁缬榭榍颉躞鲑骱"));
                    _chinesevspinyin.Add(new Item("xin", "薪芯锌欣辛新忻心信衅囟馨莘忄昕歆镡鑫"));
                    _chinesevspinyin.Add(new Item("xing", "省星腥猩惺兴刑型形邢行醒幸杏性姓陉荇荥擤饧悻硎"));
                    _chinesevspinyin.Add(new Item("xiong", "兄凶胸匈汹雄熊芎"));
                    _chinesevspinyin.Add(new Item("xiu", "臭宿休修羞朽嗅锈秀袖绣咻岫馐庥溴鸺貅髹"));
                    _chinesevspinyin.Add(new Item("xu", "墟戌需虚嘘须徐许蓄酗叙旭序畜恤絮婿绪续吁诩勖圩蓿洫浒溆顼栩煦盱胥糈醑"));
                    _chinesevspinyin.Add(new Item("xuan", "券轩喧宣悬旋玄选癣眩绚儇谖萱揎泫渲漩璇楦暄炫煊碹铉镟"));
                    _chinesevspinyin.Add(new Item("xue", "削靴薛学穴雪血谑噱泶踅鳕"));
                    _chinesevspinyin.Add(new Item("xun", "浚勋熏循旬询寻驯巡殉汛训讯逊迅巽郇埙荀荨蕈薰峋徇獯恂洵浔曛窨醺鲟"));
                    _chinesevspinyin.Add(new Item("ya", "压押鸦鸭呀丫芽牙蚜崖衙涯雅哑亚讶轧伢垭揠岈迓娅琊桠氩砑睚痖疋"));
                    _chinesevspinyin.Add(new Item("yan", "铅焉咽阉烟淹盐严研蜒岩延言颜阎炎沿奄掩眼衍演艳堰燕厌砚雁唁彦焰宴谚验殷厣赝剡俨偃兖讠谳阽郾鄢芫菸崦恹闫阏湮滟妍嫣琰檐晏胭腌焱罨筵酽魇餍鼹鼽"));
                    _chinesevspinyin.Add(new Item("yang", "殃央鸯秧杨扬佯疡羊洋阳氧仰痒养样漾徉怏泱炀烊恙蛘鞅"));
                    _chinesevspinyin.Add(new Item("yao", "侥啮疟邀腰妖瑶摇尧遥窑谣姚咬舀药要耀钥夭爻吆崤崾徭幺珧杳轺曜肴铫鹞窈繇鳐"));
                    _chinesevspinyin.Add(new Item("ye", "邪咽椰噎耶爷野冶也页掖业叶曳腋夜液盅靥谒邺揶琊晔烨铘"));
                    _chinesevspinyin.Add(new Item("yi", "艾尾一壹医揖铱依伊衣颐夷遗移仪胰疑沂宜姨彝椅蚁倚已乙矣以艺抑易邑屹亿役臆逸肄疫亦裔意毅忆义益溢诣议谊译异翼翌绎刈劓佚佾诒阝圯埸懿苡荑薏弈奕挹弋呓咦咿噫峄嶷猗饴怿怡悒漪迤驿缢殪轶贻欹旖熠眙钇铊镒镱痍瘗癔翊衤蜴舣羿翳酏黟"));
                    _chinesevspinyin.Add(new Item("yin", "茵荫因殷音阴姻吟银淫寅饮尹引隐印胤鄞廴垠堙茚吲喑狺夤洇湮氤铟瘾窨蚓霪龈"));
                    _chinesevspinyin.Add(new Item("ying", "英樱婴鹰应缨莹萤营荧蝇迎赢盈影颖硬映嬴郢茔荥莺萦蓥撄嘤膺滢潆瀛瑛璎楹媵鹦瘿颍罂"));
                    _chinesevspinyin.Add(new Item("yo", "哟育唷"));
                    _chinesevspinyin.Add(new Item("yong", "拥佣臃痈庸雍踊蛹咏泳涌永恿勇用俑壅墉喁慵邕镛甬鳙饔"));
                    _chinesevspinyin.Add(new Item("you", "幽优悠忧尤由邮铀犹油游酉有友右佑釉诱又幼卣攸侑莠莜莸尢呦囿宥柚猷牖铕疣蚰蚴蝣蝤繇鱿黝鼬"));
                    _chinesevspinyin.Add(new Item("yu", "蔚尉迂淤于盂榆虞愚舆余俞逾鱼愉渝渔隅予娱雨与屿禹宇语羽玉域芋郁吁遇喻峪御愈欲狱育誉浴寓裕预豫驭禺毓伛俣谀谕萸菀蓣揄圄圉嵛狳饫馀庾阈鬻妪妤纡瑜昱觎腴欤於煜熨燠肀聿钰鹆鹬瘐瘀窬窳蜮蝓竽臾舁雩龉"));
                    _chinesevspinyin.Add(new Item("yuan", "鸳渊冤元垣袁原援辕园员圆猿源缘远苑愿怨院塬芫掾圜沅媛瑗橼爰眢鸢螈箢鼋"));
                    _chinesevspinyin.Add(new Item("yue", "乐说曰约越跃钥岳粤月悦阅龠哕瀹栎樾刖钺"));
                    _chinesevspinyin.Add(new Item("yun", "员耘云郧匀陨允运蕴酝晕韵孕郓芸狁恽愠纭韫殒昀氲熨筠"));
                    _chinesevspinyin.Add(new Item("za", "匝砸杂扎咋拶咂"));
                    _chinesevspinyin.Add(new Item("zai", "栽哉灾宰载再在崽甾"));
                    _chinesevspinyin.Add(new Item("zan", "咱攒暂赞拶瓒昝簪糌趱錾"));
                    _chinesevspinyin.Add(new Item("zang", "藏赃脏葬驵臧"));
                    _chinesevspinyin.Add(new Item("zao", "遭糟凿藻枣早澡蚤躁噪造皂灶燥唣"));
                    _chinesevspinyin.Add(new Item("ze", "责择则泽咋仄赜啧帻迮昃笮箦舴"));
                    _chinesevspinyin.Add(new Item("zei", "贼"));
                    _chinesevspinyin.Add(new Item("zen", "怎谮"));
                    _chinesevspinyin.Add(new Item("zeng", "增憎曾赠缯甑罾锃"));
                    _chinesevspinyin.Add(new Item("zha", "查扎喳渣札轧铡闸眨栅榨咋乍炸诈柞揸吒咤哳喋楂砟痄蚱龃齄"));
                    _chinesevspinyin.Add(new Item("zhai", "翟择摘斋宅窄债寨砦瘵"));
                    _chinesevspinyin.Add(new Item("zhan", "颤瞻毡詹粘沾盏斩辗崭展蘸栈占战站湛绽谵搌骣旃"));
                    _chinesevspinyin.Add(new Item("zhang", "长樟章彰漳张掌涨杖丈帐账仗胀瘴障仉鄣幛嶂獐嫜璋蟑"));
                    _chinesevspinyin.Add(new Item("zhao", "朝招昭找沼赵照罩兆肇召爪着诏啁棹钊笊"));
                    _chinesevspinyin.Add(new Item("zhe", "遮折哲蛰辙者锗蔗这浙着谪摺柘辄磔鹧褶蜇赭"));
                    _chinesevspinyin.Add(new Item("zhen", "珍斟真甄砧臻贞针侦枕疹诊震振镇阵帧圳蓁浈溱缜桢椹榛轸赈胗朕祯畛稹鸩箴"));
                    _chinesevspinyin.Add(new Item("zheng", "蒸挣睁征狰争怔整拯正政症郑证诤峥徵钲铮筝鲭"));
                    _chinesevspinyin.Add(new Item("zhi", "识芝枝支吱蜘知肢脂汁之织职直植殖执值侄址指止趾只旨纸志挚掷至致置帜峙制智秩稚质炙痔滞治窒卮陟郅埴芷摭帙徵夂忮彘咫骘栉枳栀桎轵轾贽胝膣祉祗黹雉鸷痣蛭絷酯跖踬踯豸觯"));
                    _chinesevspinyin.Add(new Item("zhong", "中盅忠钟衷终种肿重仲众冢忪锺螽舯踵"));
                    _chinesevspinyin.Add(new Item("zhou", "舟周州洲诌粥轴肘帚咒皱宙昼骤荮啁妯纣绉胄碡籀繇酎"));
                    _chinesevspinyin.Add(new Item("zhu", "属术珠株蛛朱猪诸诛逐竹烛煮拄瞩嘱主著柱助蛀贮铸筑住注祝驻丶伫侏邾苎茱洙渚潴杼槠橥炷铢疰瘃褚竺箸舳翥躅麈"));
                    _chinesevspinyin.Add(new Item("zhua", "挝抓爪"));
                    _chinesevspinyin.Add(new Item("zhuai", "拽转"));
                    _chinesevspinyin.Add(new Item("zhuan", "传专砖转撰赚篆啭馔沌颛"));
                    _chinesevspinyin.Add(new Item("zhuang", "幢桩庄装妆撞壮状奘戆"));
                    _chinesevspinyin.Add(new Item("zhui", "椎锥追赘坠缀惴骓缒隹"));
                    _chinesevspinyin.Add(new Item("zhun", "谆准饨肫窀"));
                    _chinesevspinyin.Add(new Item("zhuo", "捉拙卓桌琢茁酌啄着灼浊倬诼擢浞涿濯焯禚斫镯"));
                    _chinesevspinyin.Add(new Item("zi", "吱兹咨资姿滋淄孜紫仔籽滓子自渍字谘茈嵫姊孳缁梓辎赀恣眦锱秭耔笫粢趑觜訾龇鲻髭"));
                    _chinesevspinyin.Add(new Item("zong", "鬃棕踪宗综总纵偬枞腙粽"));
                    _chinesevspinyin.Add(new Item("zou", "邹走奏揍诹陬鄹驺鲰"));
                    _chinesevspinyin.Add(new Item("zu", "租足卒族祖诅阻组俎菹镞"));
                    _chinesevspinyin.Add(new Item("zuan", "钻纂攥缵躜"));
                    _chinesevspinyin.Add(new Item("zui", "嘴醉最罪蕞觜"));
                    _chinesevspinyin.Add(new Item("zun", "尊遵撙樽鳟"));
                    _chinesevspinyin.Add(new Item("zuo", "撮琢昨左佐柞做作坐座阼唑嘬怍胙祚砟酢"));
                    #endregion
                }
                return _chinesevspinyin;
            }
        }

        public static string GetPinyinByChineses(string chinese)
        {
            if (ChineseVsPinyin == null) return "";
            string text = string.Empty;

            for (int i = 0; i < chinese.Length; i++)
            {
                text += GetPinyinByChinese(chinese[i].ToString());
            }
            return text;
        }

        public static string GetPinyinByChinese(string chinese)
        {
            if (ChineseVsPinyin == null) return "";
            string text = "";
            foreach (Item o in _chinesevspinyin)
            {
                if (o.Value.IndexOf(chinese) > -1)
                {
                    text = o.Text;
                }

            }
            if (text == "") text = chinese;
            return text;
        }

    }
}
