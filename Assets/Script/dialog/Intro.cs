using TMPro;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public static Intro instance;

    public GameObject task;
    public GameObject text1;
    public TextMeshProUGUI intro;

string[] babak1_kamar =
{
    "Pak Alif : Pagi, Nak!",
    "Arik : Pagi, Pak!",
    "Pak Alif : Hati-hati di jalan.",
    "Arik : Iya, Pak. Terima kasih."
};

string[] Tomas =
{
    "Pak Tomas : Arik, sini sebentar.",
    "Arik : Iya, Pak. Ada apa?",
    "Pak Tomas : Bapak lihat banyak sampah berserakan di sepanjang jalan.",
    "Arik : Oh, iya Pak. Kelihatannya memang cukup banyak.",
    "Pak Tomas : Bapak ingin kamu membantu membersihkan sampah-sampah itu.",
    "Arik : Jadi saya harus membersihkan jalan itu, Pak?",
    "Pak Tomas : Betul. Tolong kumpulkan semua sampah yang berserakan dan buang ke tempat sampah.",
    "Arik : Baik, Pak. Saya akan segera membersihkannya.",
    "Pak Tomas : Bagus. Jangan lupa hati-hati saat membersihkan.",
    "Arik : Siap, Pak Tomas!"
};

string[] babak1_jalan =
{
    "Arik: Pak, maaf... sampahnya jangan dibuang ke kali, Pak.",
    "Pak Darno: Heh? Ngomong sama siapa kamu, Nak?",
    "Pak Darno: Buang di sini udah dari dulu.",
    "Arik: Tapi kan bisa banjir, Pak—",
    "Pak Darno: Kamu masih kecil, mana ngerti urusan orang gede."
};

string[] babak_sampah =
{
    "Leon : Rik, kok banyak banget sampah di sekitar sini?",
    "Arik : Iya, Bang. Padahal tempat sampah sudah disediakan.",
    "Leon : Masih banyak orang yang buang sampah sembarangan.",
    "Arik : Kalau terus dibiarkan, lingkungan bisa jadi kotor.",
    "Leon : Benar. Sampah juga bisa menyumbat selokan dan menyebabkan banjir.",
    "Arik : Berarti kita harus mulai menjaga kebersihan dari diri sendiri.",
    "Leon : Betul, Rik. Buang sampah pada tempatnya itu hal kecil, tapi penting.",
    "Arik : Kalau begitu, ayo kita bersihkan sampah yang ada di sekitar sini.",
    "Leon : Siap! Bersama-sama menjaga lingkungan."
};

string[] babak1_sekolah =
{
    "Arik : Pagi Pak...",
    "Pak Udin : Mau pergi sekolah nak?",
    "Arik : Iya, Pak?",
    "Pak Udin  : Kalian tahu sesuatu tentang sampah ini?",
    "Arik : Tidak, Pak. Kami juga baru melihatnya.",
    "Pak Udin  : Hati-hati.",
    "Arik : Kenapa, Pak?",
    "Pak Udin  :  Saya melihat seseorang berkeliaran di sekitar sini tadi malam.",
    "Arik : Seseorang?",
    "Pak Udin : Ya, tapi saya tidak melihat wajahnya dengan jelas.",
    "Arik : Apakah dia melakukan sesuatu?",
    "Pak Udin : Saya tidak tahu.",
    "Pak Udin : Setelah dia pergi, sampah mulai bermunculan.",
    "Arik : Jadi mungkin ada hubungannya?",
    "Pak Udin : Mungkin. Tapi jangan membuat kesimpulan terlalu cepat.",
    "Arik : Baik, Pak. Terima kasih atas informasinya."
};

string[] ojol =
{
    "Pak Wowo : Aduh... semakin hari semakin banyak saja sampahnya.",
    "Arik : Pak, apakah sampah ini sudah ada sejak kemarin?",
    "Pak Wowo : Sepertinya baru beberapa hari terakhir.",
    "Arik : Bapak tahu siapa yang membuangnya?",
    "Pak Wowo : Tidak tahu.",
    "Pak Wowo : Tiba-tiba saja sampahnya sudah menumpuk.",
    "Arik : Aneh sekali."
};

string[] babak2_setelah_anjing =
{
    "Pak Karno : Tolong hati-hati melewati jalan itu!",
    "Arik : Kenapa, Pak?",
    "Pak Karno : Banyak sampah berserakan di sana.",
    "Arik : Oh, baik Pak. Terima kasih.",
    "Pak Karno : Sepertinya ada yang sengaja membuang sampah di sana.",
    "Arik : Sengaja?",
    "Pak Karno : Saya tidak yakin, tapi rasanya memang begitu.",
    "Arik : Hmm... semakin mencurigakan."
};

string[] babak2_sekolah =
{
    "Arik: Tadi ketemu petugas kebersihan. Dia frustrasi banget.",
    "Nisa: Itu masalah sistemik.",
    "Reza: Terus kita mau ngapain?",
    "Arik: Kita perlu cara lain..."
};
string[] babak3_awal =
{
    "Arik: Aduh, ada anjing liar. Terpaksa lewat pasar."
};

string[] babak3_berhasil =
{
    "Arik: Akhirnya... napas lega."
};

string[] babak3_muntah =
{
    "Reza: Bro, muka lo pucat banget.",
    "Arik: Lewat pasar tadi... bau parah.",
    "Sari: Astaga...",
    "Nisa: Pedagangnya nggak mau bayar kebersihan.",
    "Bagas: Lingkaran setan."
};

string[] babak3_diskusi =
{
    "Arik: Kita butuh cara lebih konkret.",
    "Nisa: Kita kumpulin data.",
    "Reza: Atau kita viralkan?",
    "Bagas: Tapi siapa yang dengerin kita?",
    "Sari: Kita speak up bareng-bareng.",
    "Arik: Iya. Kita coba."
};

string[] babak4_rencana =
{
    "Arik: Sore ini kita speak up ke warga dan Pak RT.",
    "Reza: Siap!",
    "Nisa: Gue udah siapin data.",
    "Bagas: Strateginya?",
    "Sari: Kita sopan dulu.",
    "Arik: Setuju."
};

string[] babak4_warga =
{
    "Arik: Permisi Bu, mau ngomong soal sampah di kali.",
    "Pak Rizky: Anak-anak ngapain ngurusin sampah?",
    "Arik: Ini penting, Bu.",
    "Arik: Ini datanya, Bu.",
    "Pak Rizky: Wah... beda banget ya.",
    "Arik: Kita mau minta Pak RT gerak."
};

string[] babak4_pakrt =
{
    "Arik: Pak... bisa minta waktu?",
    "Pak RT: Ada apa?",
    "Arik: Kali makin parah, Pak.",
    "Pak RT: Bukan sekarang waktunya!",
    "Arik: Tapi kalau nunggu terus?",
    "Pak RT: Kalian anak-anak jangan ngatur!",
    "Arik: Pak, kami cuma—",
    "Pak RT: Sudah! Pergi!"
};

string[] babak5_OjolDiPasar =
{
    "Arik: Bang, kok pasar sepi ya hari ini?",
    "Ojol: Iya, Dik. Dari tadi orang-orang pada buru-buru pulang.",
    "Arik: Memangnya ada apa?",
    "Ojol: Katanya hujan besar bakal turun lagi.",
    "Arik: Hujan lagi?",
    "Ojol: Nah itu. Selokan sama kali banyak yang penuh sampah.",
    "Arik: Kalau airnya nggak lancar bisa bahaya ya, Bang?",
    "Ojol: Betul. Daerah sini sering kebanjiran kalau hujannya deras terus.",
    "Arik: Semoga aja nggak sampai banjir lagi...",
    "Ojol: Semoga. Tapi lebih baik siap-siap dari sekarang."
};

string[] babak5_banjir =
{
    "Reza: Bro... itu apa?",
    "Nisa: Banjir.",
    "Bagas: Kali meluap...",
    "Sari: Rumah warga..."
};

string[] babak5_klimaks =
{
    "Arik: Bapak-Ibu... boleh saya ngomong?",
    "Arik: Ini bukan soal umur.",
    "Arik: Ini soal kali yang kita kotori bersama.",
    "Arik: Ini bukti foto-fotonya.",
    "Pak Rizky: Ya ampun...",
    "Pak Darno: Kita yang buang sampah...",
    "Pak RT: ...Bapak harusnya dengerin kamu dari dulu.",
    "Pak RT: Ayo kita bersihin kali."
};
string[] babak5_setelahBanjir =
{
    "Reza: Liat... airnya masih belum surut semua.",
    "Sari: Kemarin rumah gue hampir kemasukan semua.",
    "Nisa: Sekarang orang-orang baru sadar ya...",
    "Bagas: Dulu dibilangin, nggak ada yang denger.",
    "Reza: Pak RT aja sekarang turun langsung.",
    "Sari: Ya karena udah kejadian.",
    "Nisa: Tapi ya... setidaknya sekarang mulai bergerak.",
    "Bagas: Nggak bisa nunggu lagi.",
    "Bagas: Kita yang mulai duluan.",
    "Arik: ...",
    "Arik: Masih berantakan sih.",
    "Arik: Tapi sekarang nggak sendiri lagi.",
    "Arik: Jalannya masih panjang...",
    "Arik: Tapi... udah dimulai."
};

    private string[] dialogAktif;
    private int index = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        text1.SetActive(false);
        task.SetActive(false);
    }

    void Update()
    {
        if (task.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            NextDialog();
        }
    }

  
    public void StartDialog(string[] dialog)
    {
        if (dialog == null || dialog.Length == 0) return;
        Time.timeScale = 0;
        dialogAktif = dialog;
        index = 0;

        task.SetActive(true);
        text1.SetActive(true);
        intro.text = dialogAktif[index];
    }


    void NextDialog()
    {
        index++;

        if (index < dialogAktif.Length)
        {
            intro.text = dialogAktif[index];
        }
        else
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        Time.timeScale = 1;
        task.SetActive(false);
        text1.SetActive(false);
        dialogAktif = null;
    }

    public void Babak1_Kamar()
    {
        StartDialog(babak1_kamar);
    }

    public void Babak1_Jalan()
    {
        StartDialog(babak1_jalan);
    }

    public void Babak1_Sekolah()
    {
        StartDialog(babak1_sekolah);
    }

    public void Ojol()
    {
        StartDialog(ojol);
    }

    public void Babak2_Sekolah()
    {
        StartDialog(babak2_sekolah);
    }
    public void Babak2_SetelahTahanBau()
    {
        StartDialog(babak3_berhasil);
    }
    public void PakAris()
    {
        StartDialog(babak2_setelah_anjing);
    }
    public void PakRT()
    {
        StartDialog(babak4_pakrt);
    }
    public void Babak4_Warga()
    {
        StartDialog(babak4_warga);
    }
    public void banjir()
    {
        StartDialog(babak5_setelahBanjir);
    }
    public void Leon()
    {
        StartDialog(babak_sampah);
    }
    public void Guru()
    {
        StartDialog(Tomas);
    }
}