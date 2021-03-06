using System;
using System.Numerics;
using Xunit;

namespace Primes.Tests
{
    public class CheckerTest
    {
        private readonly BigInteger[] _smallerPrimes =
        {
            //50 digits
            BigInteger.Parse("54514624240457805546983763424841291872395601803241"),
            BigInteger.Parse("87458889974694112862431141367083178645522508789053"),
            BigInteger.Parse("72281164183436686691537904583302730691089716840409"),
            BigInteger.Parse("32534668859726429085451506054892677667571410336589"),
            BigInteger.Parse("41737719751055003552731113182677095586511478780347"),
            BigInteger.Parse("65380880004037181575458456705558140598165534201371"),
        };
        
        private readonly BigInteger[] _smallPrimes =
        {
            //100 digits
            BigInteger.Parse("5589889675023383475171881219570617986374840257604359531313617919195074679707111188655391222767122223"),
            BigInteger.Parse("2577784704462230650484479340406024390662176241353277645908514340816219700714739605664953283825141443"),
            BigInteger.Parse("5715986150590016410501646407123740012141529566394645857466641949801782939364408700469175225827349421"),
            BigInteger.Parse("4833349624682969012492808708487777076777971212695616463463334992980419015849879834711772042892994803"),
            BigInteger.Parse("3812140637744267186685073007288323218764657639775107171859044652928299427154290312084268116694576561"),
            BigInteger.Parse("5256887133564630454326454854261835174755656196112008814502774987638988124261915368866246933453333387"),
        };

        private readonly BigInteger[] _mediumPrimes =
        {
            //200 digits
            BigInteger.Parse("79620309150231191442369180075681712833728084835627597469577023377198155231410609173930178861432338142412262407672236116694467597872476034014943089370429031101068681215350324293870124835668882818563529"),
            BigInteger.Parse("36959180122288255419057586667223341893961754761284427182967682614243461469200376787892189317400503203428604789043229501263062917864813741530511503520336994691448644388860431326787351205421050297827789"),
            BigInteger.Parse("17999583577098733004583763469827424080674164783111775459806190306389943215467407368329064785107250054844254782720778492032982278795988972810281654481184033859034624197629323514634685280845332395196691"),  
            BigInteger.Parse("10534300449066548830347366714039363561037092080429041534115069819375053891064753553499730021302066682360151965385830248468223392666494726098590179957079457639051985444887175147137080733907729400077259"),
            BigInteger.Parse("13548950425299208970427826707450017410165866227969029760920446648986620730486377901526352583424219295463646884603030720161116225641764239841522511378058012374057838376324886923106899355123524589615851"),
        };

        private readonly BigInteger[] _largePrimes =
        {
            //300 digits
            BigInteger.Parse("473447352017640874472459414293013032680596323572156441721912151806453030003096461945098392252667663333840012945530972029035801779618724001180539319744821829558552788157375763328091841839200517854340939637690059595502677639357694314048180934206155612720652400260340372892107939194753677884648176024801"),
            BigInteger.Parse("654136415120319327438327021672472475928859751881841399260746021677623253790551979481078767040266951357467560903011956439020363411062216815355186365249425947092514018039850033699736287095006992790086481776667552172875125797545749868006527408219310311530278438023870997514302540711345461962496743981507"),
            BigInteger.Parse("322866441813636829663255386341249598483959651013949072284711341423369580844943923669064548784795789934333102706280725472662777285296563273031312681876565226144549513842075330600836123492657574618904455076024711869779609396530342642445594097600033201583170222329094438317865116718886355671762774983327"),
            BigInteger.Parse("516859824497029617012019891003644502896337919632033135795002648559330003731552231069280093504632867240269082289044862392358761837530824186754300990853172430919337362094888578029591023011370972139721888361946484338662564980521709744945662758264157975415803213077749006445162115261413361406784463908477"), 
            BigInteger.Parse("278542183422331337906148960566303825290775276853089544749627629196952235143777392705301098717841083114947349369318207356541708529938158433580235505763304067820545151925209926765139817963022387948712528014793385292414337220780657464000467881439151929655112545017462349125815822860003025701920541051697"),
        };
        
        [Fact]
        public void CheckSmallerPrimes()
        {
            foreach (var prime in _smallerPrimes)
                Assert.True(PrimeChecker.IsPrime(prime));
        }
        
        [Fact]
        public void CheckSmallPrimes()
        {
            foreach (var prime in _smallPrimes)
                Assert.True(PrimeChecker.IsPrime(prime));
        }
        
        [Fact]
        public void CheckMediumPrimes()
        {
            foreach (var prime in _mediumPrimes)
                Assert.True(PrimeChecker.IsPrime(prime));
        }
        
        [Fact]
        public void CheckLargePrimes()
        {
            foreach (var prime in _largePrimes)
                Assert.True(PrimeChecker.IsPrime(prime));
        }
    }
}