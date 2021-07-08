
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace resource.package
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(CONSTANT.GUID)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class PreviewAUDIO : AsyncPackage
    {
        internal static class CONSTANT
        {
            public const string COPYRIGHT = "Copyright (c) 2020-2021 by Viacheslav Lozinskyi. All rights reserved.";
            public const string DESCRIPTION = "Quick preview the most popular audio files";
            public const string GUID = "E70E3F77-8265-433E-843C-015AACF219C5";
            public const string NAME = "Preview-AUDIO";
            public const string VERSION = "1.0.1";
        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            {
                extension.AnyPreview.Connect();
                extension.AnyPreview.Register(".AAC", new resource.preview.Taglib());
                extension.AnyPreview.Register(".AIFF", new resource.preview.Taglib());
                extension.AnyPreview.Register(".FLAC", new resource.preview.Taglib());
                extension.AnyPreview.Register(".M4A", new resource.preview.Taglib());
                extension.AnyPreview.Register(".MKA", new resource.preview.Taglib());
                extension.AnyPreview.Register(".MP3", new resource.preview.Taglib());
                extension.AnyPreview.Register(".WAV", new resource.preview.Taglib());
                extension.AnyPreview.Register(".WMA", new resource.preview.Taglib());
            }
            {
                await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            }
        }

        protected override int QueryClose(out bool canClose)
        {
            {
                extension.AnyPreview.Disconnect();
                canClose = true;
            }
            return VSConstants.S_OK;
        }
    }
}
