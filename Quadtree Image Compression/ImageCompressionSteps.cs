namespace Quadtree_Image_Compression
{
    internal enum ImageCompressionSteps
    {
        None,
        LoadingImage,
        ImageLoaded,
        SplitQuadTree,
        QuadTreeCompleted,
        AnalyzingImageNode,
        SplitNode,
        StartCompressingImage,
        ImageCompressed,
        BuildImageInMemory,
        ImageCompleted,
        StartWriteImageToBitmap,
        BitmapCompleted,
        Completed
    }
}
