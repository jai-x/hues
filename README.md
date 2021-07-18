# hues

A desktop implementation of 0x40 Hues.

https://0x40hues.blogspot.com/
https://0x40.mon.im/

## respack

This is the specification for which this hues will interpret respack files.

### respack file

* A respack is a zip archive file.
* A respack MUST include an `info.xml` file
* A respack MUST include either a `songs.xml` file or a `images.xml` file`
* A respack may OPTIONALLY include both `songs.xml` and a `images.xml` file

### respack `info.xml` file

An example format of a `info.xml` is as follows:

```xml
<info>
  <name>Respack Name</name>
  <author>Respack Author</author>
  <description>Respack Description</description>
  <link>https://respack.link</link>
</info>
```

* The `info.xml` MUST include a root `<info>` element
  * The `<info>` element MUST include a `<name>` element
  * The `<info>` element MUST include a `<author>` element
  * The `<info>` element may OPTIONALLY include a `<description>` element
  * The `<info>` element may OPTIONALLY include a `<link>` element

### respack `songs.xml` file

An example format of a `songs.xml` is as follows:

```xml
<songs>
  <song name="loop_Song">
    <title>Song Title</title>
    <source>https://song.link</source>
    <rhythm>x..xo...</rhythm>
    <buildup>build_Song</buildup>
    <buildupRhythm>x.....x.x.x.x.</buildupRhythm>
  </song>

  <song>
    ...
  </song>
</songs>
```

* The `songs.xml` MUST include a root `<songs>` element
  * The `<songs>` element MUST include one or more nested `<song>` elements
    * The `<song>` element MUST include a `name` attribute containing the loop
      filename with no file extension
      * The file corresponding to the loop filename MUST be included within the
        respack archive, either in the root of the archive or in a nested directory
    * The `<song>` element MUST include a `<title>` element
    * The `<song>` element may OPTIONALLY include a `<source>` element
    * The `<song>` element MUST include a `<rhythm>` element
    * The `<song>` element may OPTIONALLY include a `<buildup>` element containing
      the buildup filename with no file extension
      * If the `<buildup>` element is present, the file corresponding to the buildup
        filename MUST be included within the respack archive, either in the root of
        the archive or in a nested directory
    * The `<song>` element may OPTIONALLY include a `<buildupRhythm>` element
      * If no `<buildup>` element is present, then the `<buildupRhythm>` element will
        be ignored

### respack `images.xml` file

An example format of a `images.xml` is as follows:

```xml
TODO
```
