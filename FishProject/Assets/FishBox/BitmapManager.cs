
public class BitmapManager{
    struct FILE_HEADER
    {
        /* type : Magic identifier,一般為BM(0x42,0x4d) */
        short type;
        int size;/* File size in bytes,全部的檔案大小 */
        short reserved1,
            reserved2; /* 保留欄位 */
       int offset;/* Offset to image data, bytes */
    }

    struct BITMAP_INFO_HEADER
    {
        int size;/* Info Header size in bytes */
        int width, height;/* Width and height of image */
        short planes;/* Number of colour planes */
        short bits; /* Bits per pixel */
        int compression; /* Compression type */
        int imagesize; /* Image size in bytes */
        int xresolution,
            yresolution; /* Pixels per meter */
        int ncolours; /* Number of colours */
        int importantcolours; /* Important colours */
    }

    public BitmapManager(byte[] data)
    {
    }
}
