﻿using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace JWTDEMO
{
    public class Header
    {
        public string alg { get; set; } = "HS256";
        public string typ { get; set; } = "JWT";

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public string ToBase64String()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(ToJson()));
        }

    }
    public class PayLoad
    {
        public string sub { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string pri_account_id { get; set; }
        public long iat { get; set; }
        public string role_policy { get; set; }
        public string scope { get; set; }
        public string iss { get; set; }
        public string exp { get; set; }
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public string ToBase64String()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(ToJson()));
        }
    }
    public class signature
    {
        public readonly Header _header;
        public readonly PayLoad _payload;
        public readonly string _securitykey;
        public signature(Header header,PayLoad payload,string securitykey)
        {
            _header = header;
            _payload = payload;
            _securitykey = securitykey;
        }
        public string GenerateToken()
        {
            if(_header==null || _payload==null || string.IsNullOrEmpty(_securitykey))
            {
                Console.WriteLine("not valid");
            }
            string headerbase64 = _header.ToBase64String();
            string payload = _payload.ToBase64String();
            var algorithms = new HMACSHA256();
            algorithms.Key = Encoding.ASCII.GetBytes(_securitykey);
            var computehash = algorithms.ComputeHash(Encoding.UTF8.GetBytes(headerbase64 + " " + payload));
            var signature = Convert.ToBase64String(computehash);
            var token = $"{headerbase64}.{payload}.{signature}";
            return token;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            string key = "ZeZfd4a0aHJWPe7Nk3RzAkxMD_uv2Z8wYrIbdHJkugoyaRpqDRNpz8HHhOPzJWbHj1xh-gg0_d6bV1Z2T9JJDLeE5ShLt-d7uRRPE2UrKed7a2EjgY7pVu9uwBHHdNYgF7A65Q7uRmEkhjzrosEVMDA3lEyasX7l_LlVO17ma84WSwY4ppJdzVfTBNfg_QAww8CJhdBkl26MwuM-ex6_3HZp6OEuJ-2hsrad6k024GKmGFiW-jQW72Yo4AwzIVPKi6NI2TXeValI4vfq9m0PJAPWKqeqHSHGo10OpvZvoxx_9E-n4hkhPNFK3qAL4n9Ma8GGrzdBrIWRCs4bnZvZxw.7NMD26zT8eq-xWpPid9weg.NXY57f_7uhOp8U8GSRdQ4EaZm7cn5hHhyqllj-AL4FIdlSI9XRF5J3yBadwu5g08P2RIgCgct97JIKGqH0YIA-VChiRgZtQgTgCQvJ0XcEu31ncea-FHHtqxvfjLn5QeiVMpYLxRL2zYAQa4MIGFa46klfhhqHF64PotFVCCibrJ3tAqzT4QGr2sLzKTs05bWCKCYpvAkKt4WrOY9V7JKb9feoxwY5AglY450Ealq0FEFhdA_NH0EHSzuaAiHrxsvjvyS3MkkHk4vMGFmZ8tNYXcDL6WnvHVlpJ3FemQrgsO6q_3FytbOfSU617ermVANUQ_4EE3gzzGQgdWVkMjQZwcedsLCujV4cfpNoSwbBnTIv6J7-5d2DJGOexKEprgbu6tD35v0kQ5Yw_XyHXdXqubHV00paCNq6gY1i0ImButAmWwJXYEev27w8G2Y6VNTfdplDhXcHZXRgeyoyMYZApbMMK4U6k_JB973leuEi-Iggh4bWPw7c4aK177bn4kYNV_Vq-GgXxQwrUDP6yPivCV4Av-LMeREWd97XOSFfgbecZYNI8Xp69PCuWKq5pIZIZF_PoJ7vZtQxRHGKGPZ2SzMXxvjv2y0veqhjgmEGTJT37dowL4UJBx5mEvqc86C78OJsJXJpHc5mODrHzkqZhKBCCbV0u2D-MN68mqLvd468oBvhR9gJLyB7EhE6oH_XuhrXbnxPFX_qSVqiKA-YmmP8gzwaECWZRaTmpkrk5eEmE8HqGuTiug3Ukv1UumC_n6IFRcnDMaLGv9v4kn--9KGoVx_NNVWaSI5NABU5kD9cD-tVR321n2tFG6xVerW3d7IP91_8xbIaPlImjoZkwhR8QzzPwa8BerU2RGRWlKMvnNQU0XhTKSY2mV_fEFfEXF51YNtk5k8OWk7CuI-IdsAxVP6yHUtB85RNwY-FejzZkOqQRQhzkAoup2GQDKCAJBEmoSGaSX5ZGmQ3IRGWBa3IyF6VUNScTNqnYrcos4g77-wrl9BxQdlptPDPZAywxQhNwDBpD1XcDym9HaIj_vz4m_uvoGKFQial_RJ1LJMaJMCnGQOZiOUQBYZyA4FnFefXXEATTl03mh84QH8DSqFsIaAuSCmA7xcATFu6-BwHmYxl7tRudDJLGVrdwuNiGLX_opYSdEsmLB-Vl1WEO3UOFIzAltowaUuLBaT-zenecfxVpw0ACqCOTlZiblYbYbBIT6OMA_ucuzZDSgmppzpOwglA8Q7iRzsSYwMGrMHaBC5bjbh_q2Kp6bfrfT1gbhYwgieNZ2BnkThOAOEuQrIO7xG5yqjNTpWK9vJzDav5cz8k3CLaXi1U2AOfzXququp198yT6dGixsvqr3eTYBwGJH7Wr0Hq9Zt7aAbDK9N7EiMGqWWSwTerAi0OSpe4MSbBeVjl7WltooCdJKoml7kjCwz0mco36E817V2mQT7w2Rf8V-6xO4qHnmAcH51D8f9KKQuZJnwLrsO_rp881lSJCNIouBoBYoWmShvC-1mG9Gs59JfQ1GFuQzeGciX4dYHsfhbNNxe9-qfc6cNqzduTEPZajsNmQlgralFQ6LU6NX_yY1Noldx4sLem96HSaOHXY4ha8uiEBGgnUtdd7RnsNNCOXERuEZP_PI1wb4tyAKbFqAMtPAMklTAkzs4c1v-SdhyWF5fEVseVADBBMLtMJZh49n1lb79-NRLPuQ7XHoZXD-0GNUwPFlM24RH-YR2iOH0S0E0CTjuBHwiIo2qW6Z60LqpGxeudaK5uZ03kOKj0knAhbY2yd8e8JgN-iZZNiBp7YVsDPHE9_RHL6crkIcfURD7z7-GO83D-ZNlydWUbVmcJ2a0VDPcrLZpiUUtYi0lfPdqwm1ADvTH6AmQDlICLc7rOT2Ey6oLtTmFSmIRde2WMYHnuk9j-t409vWl2U6a7IhFsPN8biHxqQRjft3i4yaKev7yQUTK85vtDa-SqK2SeWBkg-t0NLYRd8_UtlIdQ9VOboYpEXfrqPEYWtSMUMe389dInCW0RZ2HVGVthVD7Acrs0fsJ8jm7IKbBGV5RrlV8xAINIVrPS3w2sbZD0Ao7IM6ZvbRiF4473rdnVYWheGsbLNoU6Bkwz-rpR0N3G9egeSIrBpeDwL1Odbw70Fst3mdlAbDox0_DvUD9M6glCUkPpj2BKmahsKiU8KmnApaiDF9ezdM3LWJKIK9ERlB8krF0VYkdRZZDKRI_kHKJf5ZubLLMZdfvtSDptJ_zBKWJ6nkkgrQbwLrdhbY8NGO3mqDUtbD8JpfQBYq2y484vr5ZB1g_FZ395g-1z4vt2CMgim51owRnLJa5Vms7jE3k96muD64jfwtelJsMEIkhCT64A2miS6BKvcprkJ7_391-mLwcvzVzydzN1RC9pPKjEdt-SSauOV2uAGn20aeziuMFMvmAaikoNOZK2ojxiDxMGOIBcmvmWojT1nPHBXnS2veeL6SIs90vuv-aBQHKUHG-jjUu2xCvudYm8d8lD4zscfpUEHh18-tJJW3X8AFoYxQUSs9iH_FsMDyRdk7dsdIjlcGcuxbHm7DSyj0vYUJcN5eq01TKPrp4bVmU3q0oY7zhpRQZmKFsCXyi8CCEoligFLMhTuTr6AtOQXdAk-YmYFtD2iBhPZvGhe-0JXZuk1SPRxQb00aNbg7jHu7s8hmKqjvTBRtz0xspDyWJ7a7FvxRlyg7J3TA9Nyd7pZkmDSvpUwsIFzkfS1QFgZEsDvcvkXV08dCz8zO7dQoWm1NU9df6vCEx4Dr2Aym5jrjUFtl6ChRIbuW9S9UuWJK5VPRYiFp96truBuXTRBPk6rm17eSyZ8WPuuGI-GRcprRcuW_Nn5nZE_5Glqj0zvJt6Qm2QQe_Qo4Z8FDpB4NP6Dh3lwVNlIndkEEHa_StHz2PKs13FXKRvw3pOcv_8uXWXI5MW8LBAXezQQU-cz68p1MKMEae5MsdjYdGR9J3BjTNlHlD7yR12uZgZh4jWWbULvUTv7No0iBxXirUBhCuCsPGufMhmMiPPmUObagPo-zkEJB4ju0baYWZyQF0Tr9YgQItI2RDnDU76UgGvAxOyC7nWYnNSmoW9f6Uwz20tRbK9RWtACwf-3yB63mcYGr-8qpRnziyVibNOvYpk0PKh52LancKbYyn9ZD-tIuYVx0Do89e7joNCKPAkcnxrCM6kAw1Crl0PsZS0qx5A8x2v1TijL2JczqEl5a2HAxiggvAp9dC8LhZ78KZ8kDFQ350ItHX6pbLwdGnvd4tDwMbOwIrpzov-lynEypOKDB9UxXDzV0C1tbM30naD0R1E1AjZmKJax9wgz7pQAQdH-LfeSNqSrYK8cpwJ0oejo13WOJiod7_pK4aiJQKoeYepX3JVc4sXdW6h9XqMWzSaqY38hPk4Qk1jZNaGwSjqpY9HcHr8Qlu3g5fRmGzEjXM2YLtCQK6C-RjCazRMp1Q9zMhplPrHjy0O3q2MneawpP70_tVjyxHlXFQawEpqXxsgwj-RmRuAmw9JoHS4rX1v0uu8sXNke_Jc_vad2SeqZfRHZf9ZWMACvyW8Xx-BfycwI9JO09jFvupOQA3KkiBDQhYDGcIRbOSxTa-QHDyxhS5QGDY_zEXimicm_yPuD_hQt3iTOiBdj5yM1R3aAxQnyJJr-Rl6Gje5a_537XbPsdO2dGcKkpLr06loYnjkQ5p38zWDZieKGRMTIr0WR0_mH11Lji5M8yoy1wZjO3QV5Rk9FaBqUFGsvlntWs-9gQ63j4iNqizGD8-OuFSm0-Gf4qK8s6ml8JslvJd1GSo5qfYpMnnHWKygPFErd2iCY73oS3MX0BzSlyvz6px1FCVuyRWBgrLESgcNHSkK6QYbkyjE6r7rDrB7LvRiqopz-pAlRHGuAZV1cAdAc4EsDJUEvqHPA-UGb7gkNYKt6nPk3dsIkRO-777Dxchtbjxun0L4LEugBDRDEwtyMbOuPiWWOcpK24ymU7m-_PCwGuMgomf6PCW7ENTnytgI-Bn8FSFZBgH46m0V9YSfCOtmB_MFocU64h5k3Gkg9hyInqDKlzPavy84TmoO1cXIcy4sUVQ26RSwTEs6Ib175lDdVVW3go0jlbVr6KPl2mbcyUuP_igZOi3hONMTWu-3-c6qXaX7YxYKOIk-kCl7hps98JgbuqQ1JL1TpTTKvm2VsErvxb3fMsgGr4gFM6JSTu3qyGRQCN2VyjuXgldWGg9c2qJVTfv1Avzc91YXYezuzxFLHgZ9j0ZwGlnRY-S2o5x2cqeacLp86MMrT3La253RPVtJzZRuGZlfpOp2BhheAiprKyT2Xcn1KWwlp7TCXRoWq_Xzu4m5CVhy-9ubNBQvuSL00G46k3K2yiJwwO3kUyNgUhGn2EFKopf7_UIhL_g2m4Lz6Pp3GcydWZbqC62M47Kj1fXjKOIT4SiJmmiX5Uw1-pH7HHmhr5oxcgU9BXu--Eya06kFaf5pUnoUQFr7K0ohvdCvWlsQi6wdrjtkKXbTD9IElebWRyeFFRUADnnbgqsne6z3nHMlyMaHD24DmLgzCSIIicxzXFNb7j2N_ICgg0rAcmqmUOUv_92NJcr2WlcELrsTH0h83Vh0keOY7a4aUfmMDZXaCzZu9WSYAxpk3j8c-nYlEcnQM1qRKrt-65Oi_-PfZCcy26ugabwQ0wkT2hQb6879M5qoS9ZHs5Nyh_HieAy36bWej4DZXLgDRJtnx7lVoI6eLV66udCkpK4_7F6rMikvzuRWtdPXN0P--nvpiZU3apdiYLPxKgIOvDK8bNl_DWvMElGqi4jFQpKbDOve3dVDGPlccsilkVf2WaXa9o0Gxu2_T0dtb7VvCnN0nQfuRTklIjVfQgb5yVjvbyJlU_qK0upaYPH02X7O8yCo07z75X8NZPCxOcHb-Y038vzw70BxfoFf4PrAjSMXxtVaV2vU1naKKU-RE63YvksI4blbcZ58LS8hrUiKMJosvqYq-eh3Z1_Y2miZURSemV5reqnhY9yiMeb0sjcMsIWz9Jv9oGdXA9MfrhVJeb_l8aPn9yo1-RUDuig-nzpRQgjFqGksyIwdihNbsTv1ExVV56tBnngtrXz07tVZhc2Vo9BfieOtLLe2rV6KkB0Cb4G2I7NeZ4CsHJ9SvFKqOVCdhE80C102D9aqKsAsItWeHOioUFDPGeFS68j_TYgj7t7Eu_fFgdIQkhHGOCHKPc8OkuG7MPW9lYEH3nFTQ1ed-udBEsO8iuESUGdGHDOHiZLNw3AHGS3ZkVCgQjUhsIbx0oi6XuQiwZK_VSV94wY7YgKBZHbWWq2YBQxFaITXZIRdDIMI4um3Rfie1teVFy4XAENlvtbL4Mnlqbl1W6qqV08NoWc42MnSTZHSqh12ZRqVaWG_dJyUDmRIT8IdiF8Kzj9v-ZxM_-1T6vhanNkvtv2lU1U88-1XYBlRDccmfV0GaDjOiuF5S30PHdzLhFFwfiZs-NM5uQcGc-1bTC2adzyA-kll-eLFmSsRFVhdPuRvxuVudsdy8oDbrg4SyAbOBciRYmUG3NN_EHp0eAMQZFfqMesUHSLmKnzq4nCIokINlsMk2ffLNQrLTdTpFhsodEEtN8bGL6n-LO039kfJb4_XBX4wONZiNGHI5Hf5xWjNrHfMfll94VMOyqyDCfsvSC0VQc3ElahsYxeH9BOG2VIA8WfbzLRbRCxAmEim8uoBDDI5-5ZjDFi2Pu_XQklusJl9aAm4xYtGBkHSA9A3TWqgye2U9INTX3QnjcFVtPu6RaNz1c2mci88h5LMEglF6HR2Xtd7nPeGbC6fuGVUYqUm1wZFBcIcdqmiLH5OCxJ-i51dMcdSnlWFqXj6uysh9MkRIaDCeJm1WDBT8Stc-Do_Wjltg6BDsSA4ezML5hLfN9SdE7Qe76TyGqWV9uxrSIRZ4quf7YJDA0zMZrGfPhc_wMvijy0tqAVNpZoBoCrw5INxd_cJEHyS7FhuPFV7z3YgcXUGoViD4CyQuOhR4fTcP6jH_x5nJ7ItBQJ7HTctAa6C4_hQmrc5yJLe9xr9UezOSxZnI9ERF4PiwZs2Ayctfy6FI5MeXjsrSgEjwAMkQIlRjstGPqfoYG7TAbL-GI8LqHxg7MrvdFmcDmhRZfMI5OkH7jaR9V1kkcdkWOvrmD0JZJxKtzn6NMkLnY4xKpJgsGs9iU2Khi-RIV1VC_YSUr0HyFqQgQLLs-3SvVe0RmJ35j4CZ74krF6gpELaMTK3nmmSr8Zj5n8QpOd607UwSo8q76or0eork-wdFK9V-xZYpsYlIBIHcHg6URnGb5vjTMVaCQtaP9gzgYqiowGcUPAE6q84m9qaeIjUStNqbBqhtb8OCDdvNnNDRRIi_3Tp1FP6HiWElKRwiEOrB_WrXow984sONKVv86Eh2BO0is_6YosLQqm1OfnTlgdjUp571hdjnZQaIhehO5kA8_bfxerJIOSDk2x9wnVqsnAqCch70k6WdLzmVlOG9ja-PAnMm9MkxmfEqtXDGHWdZHYXeXwY4qwyWEhKofP5L_07JZf9GjZhyZvLouNVOhe3e1gZtGxAmGVwAkLhYHn739oAGU0E91iofh-rnqnk70FgUIlMQE2rxd8Ek6YqpjNaS70v_MQm65yQXt3kcNmjEHqtRdJRfx52i8Tizu3p3mSr8cJRfJe9tp2TBnVpb2O8p2iwCARJinVoKuW6K3376Sfg3fOLhVFbOhq1hjEfspkrCIQYzNntIo5b1DpdgYGrkGQvWYYFLwZky8zrEw5SA8FcjQhfxCuGZHh3luPGMGENr928aHlke2DBSyTS8EOXVYNpYDxPoKJIGn1bePQGNQoSh5PDBb1B5jEW3e1ROhy96-d9VXbiK8qdomBHZH25lX2QPywo23pfAldWkkEokHfOZ8KHXZKMzvvQSl9sVbuXti-L490rAfc7esoaA7hfByuSKFdZhi1SJZUtzKjBQQMJWDvbWrFjMKbRIZv06P2HjJ0QmgM5jlCSbTFrH90X0ZGG1rhSLx7dx9qkSEdzrg3XWlMBR-205X6lllySMCrCrc4Az32jpclIKCyF4IadzND99aYYayQD5nPvhM2qJNtnL8QZGJOYHSSy_zLKPPi9xbgp26eNoUeooHF7WvJpE7iAmabRbhoNT2M7iOA7RI6qSzvMbtmCOaZ0gzZnBV-KXk1WHaBocf4TQ2tGs8BvPwdhenzrNp0xATRwrz8CIip6zg_hL_VHtqILNPnO75HSNSDsxRI_0h0L2-gu_Kw445A6-j4ix91u0VLAFWoX7P-vST-7BfeSDT1XA8O5CU1kJkVMlUBuDpKajrk85kDKwGrdfJrMwu3PZ-RSB3w-Q0kp6vHF1lCHP-CX5DZYuHLAXUs93ww_tqo2kJTr5YbdNL1szwxwWf7XS29Cv2CqGn6uz_UjdyAMGZuUFKuBFEIWKw-5zX4PTTsbvb_JxoJ0zRcV7xxIK5NsGQh8iyxjqKMSC2OcYGydOIPqEsSH2q-d80TiBpmAoLR4_BtVxTI4QFKUOIkvyjG0-1XlhZcafFM3Kyc4uUbVbvU70QlEe0V55hW_AMIhPuXeyoh342NaxMJiGJmZJlkJ8OqnYmdMisSnmGGICJT_Fp5gqZBSV3ikLsmqHA6thr44IlzZ59tdR8ZIWTwUeYPKrCmAKVBl0EIfTPYS2-UfxpdagsyLBlUwBaJm5pc58vRp2c4X7p6Hqo85CCJwER0S1-Dz_aBRMuC__jtzJmYUdhiVaBH-QjfuUc42Vx3dq1z9qkFuFc3i3pBaMCOEQf8SX02bERUfQwaUaUEwsXh_EtG6s6Yg7phG4zqEsUfHuoMn_Ix9NL-va6zVKUjwF6EUahhonMEAAXdTCipBSOe3tSsYZnXWrW2HrzsgxruiAlK-3WiG1qDHO0PumRLflg31JaK0Dr3Z38GiXxoJVZe-iOYmMvtURM4ynXHSK8FnIoWeOWvYR1RalRY_-UP28WDTddvZ7k59a22zSzRKw3nOnJaNp9YK0jPofO71ctauo7lxkxeBz9fTawR17k5X8B9TeOA3JY5ZJ1DNvP_7XhzxO4twYp8NvFodNAFz4MgIgg2q2XQph7Ov3Lxjwq7g1xq24YkwdlVm35kNIMWDqm8WedokGC8wgM3sVTHC1iRXAoEhVun-xZBEAmZyzEX5OgHLCo8lpBNEBtjjwcZhQCWUaFD7SxwVNFoZVfcrlC-qlUuOZuKjG_zpRPCG2nIxh8XEf2LfcT6vB_v8i0_9ZtAQYnVTzOcrnq2BT7aIG9CMz7Z0VCzgmgP7ZvztX1ftNl0lqCj40ofGczVfhiQbz8P2UmlAko8V6JDTgq7kXxMLByyXjgFcv6odWrobg-rfvj5-6_4_c2zJ5GvS64S6BtQXxsnrtjMVJpAwNENv61oBRH1wWgoKE8hS9RRF2b12wYzdjwOEj-Xe10wDQ3k_DU5mdLMZ5yhKN9Y-wJba1F_euP_0dQpnOEk-U3LWcsyGMSg7caDTgL_L3mNOFJn-N-ZAyqFaYJHKDVvZEmmeFzyyHz5c1Zjx4N07F0B6pSfEz9-ys";


            Header header = new Header();
            PayLoad payLoad = new PayLoad();
            string uniqueid = Guid.NewGuid().ToString();

            payLoad.sub = "SesssionInformation123232";
            payLoad.iss = "http://WGJWTGenerator";
            payLoad.name = "Anuj sharma";
            payLoad.username = "Anujsharma11";
            payLoad.email = "anuj@gmail.com";
            payLoad.firstname = "anuj";
            payLoad.lastname = "sharma";
            payLoad.pri_account_id = "account id";
            payLoad.iat = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            payLoad.scope = "system-access";
            payLoad.exp = DateTime.Now.AddMinutes(5).ToString();
            signature signature = new signature(header, payLoad, key);
            var jwt = signature.GenerateToken();
            Console.WriteLine(jwt);
        }
    }
}
//microsoft.idem