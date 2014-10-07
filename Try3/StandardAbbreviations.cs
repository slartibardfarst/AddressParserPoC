using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC.Try3
{
    class StandardAbbreviations
    {
        #region Data
        private const string _streetSuffixShortList = "ally,aly,annx,anx,arc,av,ave,aven,avenu,avenue,avn,avnue,bch,bg,bgs,blf,blfs,blvd,bnd,boulevard,boulv,br,brdge,brg,brk,brks,brnch,btm,byp,bypa,byps,byu,cen,centr,cir,circ,cirs,ck,clb,clf,clfs,cmn,cmp,cnter,cntr,cnyn,cor,cors,cp,cpe,cr,crcl,crcle,crecent,creek,cres,crescent,cresent,crest,crk,crscnt,crse,crsent,crsnt,crssing,crssng,crst,crt,cswy,ct,ctr,ctrs,cts,curv,cv,cvs,cyn,div,dl,dm,dr,driv,drive,drives,drs,drv,dv,dvd,est,ests,exp,expr,expw,expy,ext,extn,extnsn,exts,fld,flds,fls,flt,flts,frd,frds,freeway,freewy,frg,frgs,frk,frks,frry,frst,frt,frway,frwy,fry,ft,fwy,gardn,gatewy,gatway,gdn,gdns,glen,glens,gln,glns,grden,grdn,grdns,grn,grns,grov,grove,groves,grv,grvs,gtway,gtwy,harbr,havn,hbr,hbrs,hgts,highway,highwy,hiway,hiwy,hl,hllw,hls,holw,holws,hrbor,ht,hts,hvn,hway,hwy,inlet,inlt,is,isle,isles,islnd,islnds,iss,jct,jction,jctn,jctns,jcts,junctn,knl,knls,ky,kys,la,lane,lanes,lck,lcks,ldg,ldge,lf,lgt,lgts,lk,lks,ln,lndg,lndng,loop,loops,mdw,mdws,mews,ml,mls,mnr,mnrs,mnt,mntain,mntn,mntns,msn,mssn,mt,mtin,mtn,mtns,mtwy,nck,opas,orchrd,oval,ovl,park,parks,parkway,parkways,parkwy,pk,pkway,pkwy,pkwys,pky,pl,pln,plns,plz,plza,pne,pnes,prk,prr,prt,prts,psge,pt,pts,rad,radl,ramp,rd,rdg,rdge,rdgs,rds,riv,rivr,rnch,rnchs,road,roads,route,rpd,rpds,rst,rte,rue,rvr,shl,shls,shr,shrs,skwy,skyway,smt,spg,spgs,spng,spngs,sprng,sprngs,spur,spurs,sq,sqr,sqre,sqrs,sqs,squ,square,squares,st,sta,statn,stn,str,stra,stream,street,streets,strm,strt,strvn,strvnue,sts,ter,terr,terrace,throughway,tpk,tpke,tr,trafficway,trce,trfy,trk,trks,trl,trls,trnpk,trpk,trwy,tunl,tunls,uns,upas,vally,vdct,via,vis,vl,vlg,vlgs,vlly,vly,vlys,vst,vsta,vw,vws,way,ways,wl,wls,wy,xrd";

        private const string _commonStreetNamesContainingAnd = @"
            Hide And Seek;     Hide & Seek;
            Birds And Bloom;   Birds & Bloom;
            Town And Country;  Town & Country;
            Towne And Country; Towne & Country;
            Brick And Tile;    Brick & Tile;
            Rod and Gun;       Rod & Gun;
            Lewis And Clark;   Lewis & Clark;
            Metes and Bounds;  Metes & Bounds;
            Ranch and Resort;  Ranch & Resort;
            Golf and Country;  Golf & Country;
            M&M; M & M;
            ";

        private const string _twoWordNumberedStreetNames = @"
            county road;county rd;
            county highway;county hwy;
            state highway;state hwy;
            state route;state rte;
            us highway;us hwy;
            township road;township rd;
            off route;off rte;
            ranch road;ranch rd;
            ";

        private const string _numberedStreetNames = @"
            highway,highwy,hiway,hiwy,hway,hwy:highway;
            rte,route:route;
            interstate:interstate;
            rd,road:road
            ";

        private const string _expandedStreetSuffixes = @"
            highwy,hiway,hiwy,hway,hwy:highway;
            rte:route;
            rd:road
            ";

        public const string _states = @"
            ak,alaska:ak;
            al,alabama:al;
            ar,arkansas:ar;
            as,american samoa:as;
            az,arizona:az;
            bc,british columbia:bc;
            ca,california:ca;
            co,colorado:co;
            ct,connecticut:ct;
            dc,d.c.,d.c,dc.,district of columbia:dc;
            de,delaware:de;
            fl,florida:fl;
            fm,federated states of micronesia:fm;
            ga,georgia:ga;
            gu,guam:gu;
            hi,hawaii:hi;
            ia,iowa:ia;
            id,idaho:id;
            il,illinois:il;
            in,indiana:in;
            ks,kansas:ks;
            ky,kentucky:ky;
            la,louisiana:la;
            ma,massachusetts:ma;
            mb,winnipeg:mb;
            md,maryland:md;
            me,maine:me;
            mh,marshall islands:mh;
            mi,michigan:mi;
            mn,minnesota:mn;
            mo,missouri:mo;
            mp,northern mariana islands:mp;
            ms,mississippi:ms;
            mt,montana:mt;
            mx,mexico:mx;
            nc,north carolina:nc;
            nd,north dakota:nd;
            ne,nebraska:ne;
            nh,new hampshire:nh;
            nj,new jersey:nj;
            nm,new mexico:nm;
            nv,nevada:nv;
            ny,new york:ny;
            oh,ohio:oh;
            ok,oklahoma:ok;
            on,ontario:on;
            or,oregon:or;
            pa,pennsylvania:pa;
            pr,puerto rico:pr;
            pw,palau:pw;
            ri,rhode island:ri;
            sc,south carolina:sc;
            sd,south dakota:sd;
            tn,tennessee:tn;
            tx,texas:tx;
            um,u.s. minor outlying islands:um;
            ut,utah:ut;
            va,virginia:va;
            vi,virgin islands of the u.s.:vi;
            vt,vermont:vt;
            wa,washington:wa;
            wi,wisconsin:wi;
            wv,west virginia:wv;
            wy,wyoming:wy;
            ";

        private const string _onePartDirectionalsMap = @"east,e:e;west,w:w;south,s:s;north,n:n";
        private const string _twoPartDirectionalsMap = @"southeast,se:se;southwest,sw:sw;northeast,ne:ne;northwest,nw:nw";
        private const string _directionTranslationMap = _onePartDirectionalsMap + ";" + _twoPartDirectionalsMap;

        //this comes from UPS: http://pe.usps.com/text/pub28/28apc_002.htm
        private const string _streetSuffixTranslationMap = @"
            allee,alley,ally,aly:aly;
            anex,annex,annx,anx:anx;
            arc,arcade:arc;
            av,ave,aven,avenu,avenue,avn,avnue:ave;
            bayoo,bayou,byu:byu;
            bch,beach:bch;
            bend,bnd:bnd;
            blf,bluf,bluff:blf;
            blfs,bluffs:blfs;
            bot,btm,bottm,bottom:btm;
            blvd,boul,boulevard,boulv,blv:blvd;
            br,brnch,branch:br;
            brdge,brg,bridge:brg;
            brk,brook:brk;
            brks,brooks:brks;
            burg,bg:bg;
            bgs,burgs:bgs;
            byp,bypa,bypas,bypass,byps:byp;
            camp,cp,cmp:cp;
            canyn,canyon,cnyn,cyn:cyn;
            cape,cpe:cpe;
            causeway,causway,causwa,cswy:cswy;
            cen,cent,center,centr,centre,cnter,cntr,ctr:ctr;
            centers,centrs,centres,cnters,cntrs,ctrs:ctrs;
            cir,circ,circl,circle,crcl,crcle:cir;
            circles,cirs:cirs;
            clf,cliff:clf;
            clfs,cliffs:clfs;
            clb,club:clb;
            common,cmn:cmn;
            commons,cmns:cmns;
            cor,corner:cor;
            cors,corners:cors;
            course,crse:crse;
            court,ct:ct;
            courts,cts:cts;
            cove,cv:cv;
            coves,cvs:cvs;
            creek,crk:crk;
            crescent,cres,crsent,crsnt:cres;
            crest,crst:crst;
            crossing,crssng,xing:xing;
            crossroad,xrd:xrd;
            crossroads,xrds:xrds;
            curv,curve:curv;
            dale,dl:dl;
            dam,dm:dm;
            div,divide,dv,dvd:dv;
            dr,driv,drive,drv:dr;
            drs,drives:drs;
            est,estate:est;
            ests,estates:ests;
            exp,expr,express,expressway,expw,expy:expy;
            ext,extension,extn,extnsn:ext;
            extensions,extns,exts:exts;
            fall:fall;
            falls,fls:fls;
            ferry,frry,fry:fry;
            field,fld:fld;
            fields,flds:flds;
            flat,flt:flt;
            flats,flts:flts;
            ford,frd:frd;
            fords,frds:frds;
            forest,forests,frst:frst;
            forg,forge,frg:frg;
            forgs,forges,frgs:frgs;
            fork,frk:frk;
            forks,frks:frks;
            fort,frt,ft:ft;
            freeway,freewy,frway,frwy,fwy:fwy;
            garden,gardn,gdn,grden,grdn:gdn;
            gardens,gdns,grdens,grdns:gdns;
            gateway,gatewy,gatway,gtway,gtwy:gtwy;
            glen,gln:gln;
            glens,glns:glns;
            green,grn:grn;
            greens,grns:grns;
            grov,grove,grv:grv;
            groves,grvs:grvs;
            harb,harbor,harbr,hbr,hrbor:hbr;
            harbors,hbrs:hbrs;
            haven,hvn:hvn;
            heights,ht,hts:hts;
            highway,highwy,hiway,hiwy,hway,hwy:hwy;
            hill,hl:hl;
            hills,hls:hls;
            hllw,hollow,hollows,holw,holws:holw;
            inlet,inlt:inlt;
            is,island,islnd:is;
            iss,islands,islnds:iss;
            isle,isles:isle;
            jct,jction,jctn,junction,junctn,juncton:jct;
            jcts,jctions,jctns,junctions,junctns,junctons:jcts;
            key,ky:ky;
            keys,kys:kys;
            knl,knol,knoll:knl;
            knls,knols,knolls:knls;
            lk,lake:lk;
            lks,lakes:lks;
            land:land;
            landing,lndg,lndng:lndg;
            lane,ln:ln;
            
            lgt,light:lgt;
            lgts,lights:lgts;
            loaf,lf:lf;
            lock,lck:lck;
            locks,lcks:lcks;
            ldg,ldge,lodg,lodge:ldg;
            loop,loops:loop;
            mall:mall;
            mnr,manor:mnr;
            mnrs,manors:mnrs;
            mdw,meadow:mdw;
            mdws,meadows,medows:mdws;
            mews:mews;
            mill,ml:ml;
            mills,mls:mls;
            mission,missn,mssn:msn;
            motorway,mtwy:mtwy;
            mnt,mt,mount:mt;
            mntain,mntn,mountain,mountin,mtin,mtn:mtn;
            mntns,mountains:mtns;
            nck,neck:nck;
            orch,orchard,orchrd:orch;
            oval,ovl:oval;
            opas,overpass:opas;
            park,prk,parks,prks:park;
            pkwy,parkway,parkways,parkwy,pkway,pkwys,pky:pkwy;
            pass:pass;
            passage,psge:psge;
            path,paths:path;
            pike,pikes:pike;
            pine,pne:pne;
            pines,pnes:pnes;
            pl,place:pl;
            plain,pln:pln;
            plains,plns:plns;
            plaza,plz,plza:plz;
            point,pt:pt;
            points,pts:pts;
            port,prt:prt;
            ports,prts:prts;
            pr,prairie,prarie,prr:pr;
            rad,radial,radiel,radl:radl;
            ramp:ramp;
            ranch,ranches,rnch,rnchs:rnch;
            rapid,rpd:rpd;
            rapids,rpds:rpds;
            rest,rst:rst;
            rdg,rdge,ridge:rdg;
            rdgs,rdges,ridges:rdgs;
            riv,river,rivr,rvr:riv;
            rd,road:rd;
            rds,roads:rds;
            route,rte:rte;
            row:row;
            rue:rue;
            run:run;
            shl,shoal:shl;
            shls,shoals:shls;
            shoar,shore,shr:shr;
            shoars,shores,shrs:shrs;
            skwy,skyway:skwy;
            spg,spng,spring,sprng:spg;
            spgs,spngs,springs,sprngs:spgs;
            spur,spurs:spur;
            sq,sqr,sqre,squ,square:sq;
            sqrs,sqs,squares:sqs;
            sta,station,statn,stn:sta;
            stra,strav,strave,straven,stravenue,stravn,strvn,strvnue:stra;
            stream,streme,strm:strm;
            st,street,strt,str:st;
            sts,streets:sts;
            smt,sumit,sumitt,summit:smt;
            ter,terr,terrace:ter;
            throughway,trwy:trwy;
            trace,traces,trce:trce;
            track,tracks,trak,trk,trks:trak;
            trafficway,trfy:trfy;
            trail,trails,trl,trls:trl;
            trailer,trlr,trlrs:trlr;
            tunel,tunl,tunls,tunnel,tunnels,tunnl:tunl;
            trnpk,turnpike,turnpk,tpke:tpke;
            underpass,upas:upas;
            un,union:un;
            uns,unions:uns;
            valley,vally,vlly,vly:vly;
            valleys,vlys:vllys:vlys;
            vdct,via,viaduct,viadct:via;
            view,vw:vw;
            views,vws:vws;
            vill,villag,village,villg,villiage,vlg:vlg;
            villages,vlgs:vlgs;
            ville,vl:vl;
            vis,vist,vista,vst,vsta:vis;
            walk,walks:walk;
            wall:wall;
            way,wy:way;
            ways,wys:ways;
            well,wl:wl;
            wells,wls:wls;
            ";


        private const string _numberedCityNamesMap = @"29 palms:twentynine palms";
        #endregion //Data

        private List<string> _dictTwoWordNumberedStreetNames;

        private Dictionary<string, string> _dictStates;
        private Dictionary<string, string> _dictNumberedStreetNames;
        private Dictionary<string, string> _dictExpandedStreetSuffixes;
        private Dictionary<string, string> _dictDirectionsMap;
        private Dictionary<string, string> _dictStreetSuffixMap;
        private Dictionary<string, string> _dictNumberedCitiesMap;
        private List<string> _allStreetSuffixes;
        private HashSet<string> _allDirectionals;
        private HashSet<string> _onePartDirectionals;
        private HashSet<string> _twoPartDirectionals;
        private List<string> _allNumberedStreetNames;
        private List<string> _allTwoWordNumberedStreetNames;
        private List<string> _allCommonStreetNamesContainingAnd;

        public Dictionary<string, string> StatesMap { get { return _dictStates; } }
        public Dictionary<string, string> NumberedStreetNamesMap { get { return _dictNumberedStreetNames; } }
        public Dictionary<string, string> ExpandedStreetSuffixesMap { get { return _dictExpandedStreetSuffixes; } }
        public Dictionary<string, string> DirectionsMap { get { return _dictDirectionsMap; } }
        public Dictionary<string, string> StreetSuffixesMap { get { return _dictStreetSuffixMap; } }
        public Dictionary<string, string> NumberedCitiesMap { get { return _dictNumberedCitiesMap; } }

        public List<string> TwoWordNumberedStreetNamesList { get { return _dictTwoWordNumberedStreetNames; } }
        public List<string> AllStreetSuffixes { get { return _allStreetSuffixes; } }
        public HashSet<string> AllDirectionals { get { return _allDirectionals; } }
        public HashSet<string> OnePartDirectionals { get { return _onePartDirectionals; } }
        public HashSet<string> TwoPartDirectionals { get { return _twoPartDirectionals; } }
        public List<string> AllNumberedStreetNames { get { return _allNumberedStreetNames; } }
        public List<string> AllTwoWordNumberedStreetNames { get { return _allTwoWordNumberedStreetNames; } }
        public List<string> AllCommonStreetNamesContainingAnd { get { return _allCommonStreetNamesContainingAnd; } }


        public StandardAbbreviations()
        {
            InitializeDictionaries();
        }


        private void LoadAllStreetSuffixes()
        {
            HashSet<string> suffixes = new HashSet<string>();

            foreach (var kvp in _dictStreetSuffixMap)
            {
                suffixes.Add(kvp.Key);
                foreach (var el in kvp.Value.Split(new[] { ',' }))
                    suffixes.Add(el);
            }

            _allStreetSuffixes = suffixes.OrderByDescending(x => x.Length).ToList();
        }

        private void LoadAllDirectionals()
        {
            var items = new HashSet<string>();
            foreach (var dir in _directionTranslationMap.Split(new char[] { ':', ',', ';' }))
                items.Add(dir);

            _allDirectionals = new HashSet<string>(items.OrderByDescending(x => x.Length), StringComparer.OrdinalIgnoreCase);

            items = new HashSet<string>();
            foreach (var dir in _onePartDirectionalsMap.Split(new char[] { ':', ',', ';' }))
                items.Add(dir);

            _onePartDirectionals = new HashSet<string>(items.OrderByDescending(x => x.Length), StringComparer.OrdinalIgnoreCase);

            items = new HashSet<string>();
            foreach (var dir in _twoPartDirectionalsMap.Split(new char[] { ':', ',', ';' }))
                items.Add(dir);

            _twoPartDirectionals = new HashSet<string>(items.OrderByDescending(x => x.Length), StringComparer.OrdinalIgnoreCase);

        }

        private void LoadAllNumberedStreetNames()
        {
            var items = new HashSet<string>();
            foreach (var street in _numberedStreetNames.Split(new char[] { ':', ',', ';' }))
                items.Add(street.Trim());

            _allNumberedStreetNames = items.OrderByDescending(x => x.Length).ToList();
        }

        private void LoadAllTwoWordNumberedStreetNames()
        {
            var items = new HashSet<string>();
            foreach (var street in _twoWordNumberedStreetNames.Trim().Split(new char[] { ':', ',', ';' }))
            {
                var el = street.Trim().ToLower();
                if (!string.IsNullOrEmpty(el))
                    items.Add(el);
            }

            _allTwoWordNumberedStreetNames = items.OrderByDescending(x => x.Length).ToList();
        }

        private void LoadAllCommonStreetNamesContainingAnd()
        {
            var items = new HashSet<string>();
            foreach (var street in _commonStreetNamesContainingAnd.Trim().Split(new char[] { ':', ',', ';' }))
            {
                var el = street.Trim().ToLower();
                if (!string.IsNullOrEmpty(el))
                    items.Add(el);
            }

            _allCommonStreetNamesContainingAnd = items.OrderByDescending(x => x.Length).ToList();
        }


        private void InitializeDictionaries()
        {
            //since AddressLineAnalyzer is static, this function is expected to be called only once. So don't need to do this
            //if (_dictDirectionsMap != null && _dictstreet_suffixMap != null)
            //    return;

            LoadDictionaryMap(ref _dictDirectionsMap, _directionTranslationMap);
            LoadDictionaryMap(ref _dictStreetSuffixMap, _streetSuffixTranslationMap);
            LoadDictionaryMap(ref _dictStates, _states);
            LoadDictionaryMap(ref _dictNumberedCitiesMap, _numberedCityNamesMap);
            LoadDictionaryMap(ref _dictNumberedStreetNames, _numberedStreetNames);
            LoadDictionaryMap(ref _dictExpandedStreetSuffixes, _expandedStreetSuffixes);
            _dictTwoWordNumberedStreetNames = new List<string>(_twoWordNumberedStreetNames.Trim().Split(';'));

            LoadAllStreetSuffixes();
            LoadAllDirectionals();
            LoadAllNumberedStreetNames();
            LoadAllTwoWordNumberedStreetNames();
            LoadAllCommonStreetNamesContainingAnd();
            //_dictstreetSuffixShortList = new List<string>(StreetSuffixShortList.Split(','));
        }


        private void LoadDictionaryMap(ref Dictionary<string, string> dict, string definition)
        {
            dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var itms = definition.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string itm in itms)
            {
                if (itm.Trim().Length <= 0) continue;
                string[] map = itm.Split(':');
                string map2Val = map[1].Trim();
                string[] aryMappedValls = map[0].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string key in aryMappedValls)
                {
                    dict.Add(key.Trim(), map2Val);
                }
            }
        }
    }
}
