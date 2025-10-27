using M3UManager.Models;
using M3UManager.Tests.Utilities;

namespace M3UManager.Tests;

public class MainTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void M3UFullParseFromLinesTest1()
    {
        M3U parsedM3U = M3UManager.ParseFromLines(TestData.Sample1M3ULinesList);

        Assert.Multiple(() =>
        {
            Assert.That(parsedM3U, Is.Not.Null);
            Assert.That(parsedM3U.Channels, Is.Not.Null);
            Assert.That(parsedM3U.Channels, Has.Count.EqualTo(2));
            Assert.That(parsedM3U.HasEndList, Is.EqualTo(false));
            Assert.That(parsedM3U.PlayListType, Is.Null);
            Assert.That(parsedM3U.MediaSequence, Is.Null);
            Assert.That(parsedM3U.TargetDuration, Is.Null);
            Assert.That(parsedM3U.Version, Is.Null);

            var channel0 = parsedM3U.Channels[0];
            Assert.That(channel0.MediaUrl, Is.EqualTo("http://0.0.0.0/hbbh/stream.m3u8"));
            Assert.That(channel0.Duration, Is.EqualTo("-1"));
            Assert.That(channel0.Title, Is.EqualTo("HDTV (720p) [Not 24/7]"));
            Assert.That(channel0.GroupTitle, Is.Empty);
            Assert.That(channel0.TvgName, Is.Null);
            Assert.That(channel0.TvgID, Is.EqualTo("HDTV.fr"));
            Assert.That(channel0.Logo, Is.EqualTo("https://o.imur.om/xyW0wD.png"));

            var channel1 = parsedM3U.Channels[1];
            Assert.That(channel1.MediaUrl, Is.EqualTo("http://0.0.0.0/hbbh/stream.m3u8"));
            Assert.That(channel1.Duration, Is.EqualTo("-1"));
            Assert.That(channel1.Title, Is.EqualTo("HDTV (720p)"));
            Assert.That(channel1.GroupTitle, Is.EqualTo("Undefined"));
            Assert.That(channel1.TvgName, Is.Null);
            Assert.That(channel1.TvgID, Is.EqualTo("HDTV.fr"));
            Assert.That(channel1.Logo, Is.EqualTo("https://y.imyr.cm/xy70wD.png"));
        });
    }

    [Test]
    public void M3UFullParseFromLinesTest2()
    {
        M3U parsedM3U = M3UManager.ParseFromLines(TestData.Sample2M3ULinesList);

        Assert.Multiple(() =>
        {
            Assert.That(parsedM3U, Is.Not.Null);
            Assert.That(parsedM3U.Channels, Is.Not.Null);
            Assert.That(parsedM3U.Channels, Has.Count.EqualTo(1));
            Assert.That(parsedM3U.HasEndList, Is.EqualTo(false));
            Assert.That(parsedM3U.PlayListType, Is.Null);
            Assert.That(parsedM3U.MediaSequence, Is.Null);
            Assert.That(parsedM3U.TargetDuration, Is.Null);
            Assert.That(parsedM3U.Version, Is.Null);

            var channel0 = parsedM3U.Channels[0];
            Assert.That(channel0.MediaUrl, Is.EqualTo("https://google.com"));
            Assert.That(channel0.Duration, Is.EqualTo("-1"));
            Assert.That(channel0.Title, Is.EqualTo("US Mobile, AL WEAR ABC 3 (A)"));
            Assert.That(channel0.GroupTitle, Is.EqualTo("United States"));
            Assert.That(channel0.TvgName, Is.EqualTo("US Mobile, AL WEAR ABC 3 (A)"));
            Assert.That(channel0.TvgID, Is.Empty);
            Assert.That(channel0.Logo, Is.Empty);
        });
    }

    // ******************************************************** //

    private static void ExtinfParseFromLinesTest(Stack<string> sampleExtinfLines)
    {
        Channel channel = FindMethodUtility.CallPrivateStaticMethod<Channel>(typeof(M3UManager),
            "DetectChannelFromExtinfItem", sampleExtinfLines);

        Assert.Multiple(() =>
        {
            Assert.That(channel.MediaUrl, Is.EqualTo("http://0.0.0.0/hbbh/stream.m3u8"));
            Assert.That(channel.Duration, Is.EqualTo("-1"));
            Assert.That(channel.Title, Is.EqualTo("HDTV (720p)"));
            Assert.That(channel.GroupTitle, Is.Empty);
            Assert.That(channel.TvgName, Is.Null);
            Assert.That(channel.TvgID, Is.EqualTo("HDTV.fr"));
            Assert.That(channel.Logo, Is.EqualTo("https://y.imyr.cm/xy70wD.png"));
        });
    }

    [Test]
    public void AttributesExtinfParseFromStackStringTest()
        => ExtinfParseFromLinesTest(TestData.SampleAttributesExtinfLines);

    [Test]
    public void TagsExtinfParseFromStackStringTest()
        => ExtinfParseFromLinesTest(TestData.SampleTagsExtinfLines);

    // ******************************************************** //

    private static void ChannelToStringTest(Channel channel, string expectedChannelString, M3UType m3uType)
    {
        ArgumentNullException.ThrowIfNull(nameof(channel), $"'{nameof(channel)}' variable value is null.");

        string channelToStringValue = channel.ToString(m3uType);

        Assert.That(channelToStringValue, Is.EqualTo(expectedChannelString));
    }

    [Test]
    public void AttributesExtinfToStringTest()
        => ChannelToStringTest(TestData.SampleAttributesExtinfChannel, TestData.SampleAttributesExtinfContent, M3UType.TagsType);

    [Test]
    public void TagsExtinfToStringTest()
        => ChannelToStringTest(TestData.SampleTagsExtinfChannel, TestData.SampleTagsExtinfContent, M3UType.AttributesType);
}