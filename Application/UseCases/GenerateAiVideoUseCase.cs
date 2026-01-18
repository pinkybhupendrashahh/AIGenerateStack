using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GenerateAiVideoUseCase
    {
        private readonly IAiScriptService _script;
        private readonly ITtsService _tts;
        private readonly IVideoGenerator _videoGen;
        private readonly IVideoComposer _composer;

        public GenerateAiVideoUseCase(
            IAiScriptService script,
            ITtsService tts,
            IVideoGenerator videoGen,
            IVideoComposer composer)
        {
            _script = script;
            _tts = tts;
            _videoGen = videoGen;
            _composer = composer;
        }

        public async Task<string> ExecuteAsync(VideoRequest req)
        {
            var script = await _script.GenerateAsync(req.Topic, req.Style);
            var audioPath = await _tts.SynthesizeAsync(script, req.Voice);
            var visuals = await _videoGen.GenerateAsync(script);
            var finalVideo = await _composer.MergeAsync(visuals, audioPath);

            return finalVideo;
        }
    }

}
