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
    "Raka : Pagi, Pak!",
    "Pak Alif : Hati-hati di jalan.",
    "Raka : Iya, Pak. Terima kasih."
};

string[] Tomas =
{
    "Pak Tomas : Raka, sini sebentar.",
    "Raka : Iya, Pak. Ada apa?",
    "Pak Tomas : Bapak lihat banyak sampah berserakan di sepanjang jalan.",
    "Raka : Oh, iya Pak. Kelihatannya memang cukup banyak.",
    "Pak Tomas : Bapak ingin kamu membantu membersihkan sampah-sampah itu.",
    "Raka : Jadi saya harus membersihkan jalan itu, Pak?",
    "Pak Tomas : Betul. Tolong kumpulkan semua sampah yang berserakan dan buang ke tempat sampah.",
    "Raka : Baik, Pak. Saya akan segera membersihkannya.",
    "Pak Tomas : Bagus. Jangan lupa hati-hati saat membersihkan.",
    "Raka : Siap, Pak Tomas!"
};

string[] babak1_jalan =
{
    "Raka: Pak, maaf... sampahnya jangan dibuang ke kali, Pak.",
    "Pak Darno: Heh? Ngomong sama siapa kamu, Nak?",
    "Pak Darno: Buang di sini udah dari dulu.",
    "Raka: Tapi kan bisa banjir, Pak—",
    "Pak Darno: Kamu masih kecil, mana ngerti urusan orang gede."
};

string[] babak_sampah =
{
    "Leon : Rak, kok banyak banget sampah di sekitar sini?",
    "Raka : Iya, Bang. Padahal tempat sampah sudah disediakan.",
    "Leon : Masih banyak orang yang buang sampah sembarangan.",
    "Raka : Kalau terus dibiarkan, lingkungan bisa jadi kotor.",
    "Leon : Benar. Sampah juga bisa menyumbat selokan dan menyebabkan banjir.",
    "Raka : Berarti kita harus mulai menjaga kebersihan dari diri sendiri.",
    "Leon : Betul, Rik. Buang sampah pada tempatnya itu hal kecil, tapi penting.",
    "Raka : Kalau begitu, ayo kita bersihkan sampah yang ada di sekitar sini.",
    "Leon : Siap! Bersama-sama menjaga lingkungan."
};

string[] babak1_sekolah =
{
    "Raka : Pagi Pak...",
    "Pak Udin : Mau pergi sekolah nak?",
    "Raka : Iya, Pak?",
    "Pak Udin  : Kalian tahu sesuatu tentang sampah ini?",
    "Raka : Tidak, Pak. Kami juga baru melihatnya.",
    "Pak Udin  : Hati-hati.",
    "Raka : Kenapa, Pak?",
    "Pak Udin  :  Saya melihat seseorang berkeliaran di sekitar sini tadi malam.",
    "Raka : Seseorang?",
    "Pak Udin : Ya, tapi saya tidak melihat wajahnya dengan jelas.",
    "Raka : Apakah dia melakukan sesuatu?",
    "Pak Udin : Saya tidak tahu.",
    "Pak Udin : Setelah dia pergi, sampah mulai bermunculan.",
    "Raka : Jadi mungkin ada hubungannya?",
    "Pak Udin : Mungkin. Tapi jangan membuat kesimpulan terlalu cepat.",
    "Raka : Baik, Pak. Terima kasih atas informasinya."
};

string[] ojol =
{
    "Pak Wowo : Aduh... semakin hari semakin banyak saja sampahnya.",
    "Raka : Pak, apakah sampah ini sudah ada sejak kemarin?",
    "Pak Wowo : Sepertinya baru beberapa hari terakhir.",
    "Raka : Bapak tahu siapa yang membuangnya?",
    "Pak Wowo : Tidak tahu.",
    "Pak Wowo : Tiba-tiba saja sampahnya sudah menumpuk.",
    "Raka : Aneh sekali."
};

string[] babak2_setelah_anjing =
{
    "Pak Karno : Tolong hati-hati melewati jalan itu!",
    "Raka : Kenapa, Pak?",
    "Pak Karno : Banyak sampah berserakan di sana.",
    "Raka : Oh, baik Pak. Terima kasih.",
    "Pak Karno : Sepertinya ada yang sengaja membuang sampah di sana.",
    "Raka : Sengaja?",
    "Pak Karno : Saya tidak yakin, tapi rasanya memang begitu.",
    "Raka : Hmm... semakin mencurigakan."
};

string[] babak2_sekolah =
{
    "Raka: Tadi ketemu petugas kebersihan. Dia frustrasi banget.",
    "Nisa: Itu masalah sistemik.",
    "Reza: Terus kita mau ngapain?",
    "Raka: Kita perlu cara lain..."
};
string[] babak3_awal =
{
    "Raka: Aduh, ada anjing liar. Terpaksa lewat pasar."
};

string[] babak3_berhasil =
{
    "Raka: Akhirnya... napas lega."
};

string[] babak3_muntah =
{
    "Reza: Bro, muka lo pucat banget.",
    "Raka: Lewat pasar tadi... bau parah.",
    "Sari: Astaga...",
    "Nisa: Pedagangnya nggak mau bayar kebersihan.",
    "Bagas: Lingkaran setan."
};

string[] babak3_diskusi =
{
    "Raka: Kita butuh cara lebih konkret.",
    "Nisa: Kita kumpulin data.",
    "Reza: Atau kita viralkan?",
    "Bagas: Tapi siapa yang dengerin kita?",
    "Sari: Kita speak up bareng-bareng.",
    "Raka: Iya. Kita coba."
};

string[] babak4_rencana =
{
    "Raka: Sore ini kita speak up ke warga dan Pak RT.",
    "Reza: Siap!",
    "Nisa: Gue udah siapin data.",
    "Bagas: Strateginya?",
    "Sari: Kita sopan dulu.",
    "Raka: Setuju."
};

string[] babak4_warga =
{
    "Raka: Permisi Bu, mau ngomong soal sampah di kali.",
    "Pak Rizky: Anak-anak ngapain ngurusin sampah?",
    "Raka: Ini penting, Bu.",
    "Raka: Ini datanya, Bu.",
    "Pak Rizky: Wah... beda banget ya.",
    "Raka: Kita mau minta Pak RT gerak."
};

string[] babak4_pakrt =
{
    "Raka: Pak... bisa minta waktu?",
    "Pak RT: Ada apa?",
    "Raka: Kali makin parah, Pak.",
    "Pak RT: Bukan sekarang waktunya!",
    "Raka: Tapi kalau nunggu terus?",
    "Pak RT: Kalian anak-anak jangan ngatur!",
    "Raka: Pak, kami cuma—",
    "Pak RT: Sudah! Pergi!"
};

string[] babak5_OjolDiPasar =
{
    "Raka: Bang, kok pasar sepi ya hari ini?",
    "Ojol: Iya, Dik. Dari tadi orang-orang pada buru-buru pulang.",
    "Raka: Memangnya ada apa?",
    "Ojol: Katanya hujan besar bakal turun lagi.",
    "Raka: Hujan lagi?",
    "Ojol: Nah itu. Selokan sama kali banyak yang penuh sampah.",
    "Raka: Kalau airnya nggak lancar bisa bahaya ya, Bang?",
    "Ojol: Betul. Daerah sini sering kebanjiran kalau hujannya deras terus.",
    "Raka: Semoga aja nggak sampai banjir lagi...",
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
    "Raka: Bapak-Ibu... boleh saya ngomong?",
    "Raka: Ini bukan soal umur.",
    "Raka: Ini soal kali yang kita kotori bersama.",
    "Raka: Ini bukti foto-fotonya.",
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
    "Raka: ...",
    "Raka: Masih berantakan sih.",
    "Raka: Tapi sekarang nggak sendiri lagi.",
    "Raka: Jalannya masih panjang...",
    "Raka: Tapi... udah dimulai."
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