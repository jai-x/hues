# hues

## ⚠️  This project is a work in progress ⚠️

A desktop implementation of 0x40 Hues from the [0x40 Hues project](https://0x40hues.blogspot.com/)  
Inspired by [mon's Webhues](https://0x40.mon.im/)

## how to use

Go to the release page for this repo and download the version for your OS.  
Extract the file and run the `hues` executable, no need to install anything.

## troubleshooting/FAQ

- *Help I changed the screen mode and now it's broken!*  
  Delete the `framework.ini` file in the base folder to reset to default
  settings.

- *How do I import a Respack?*  
  Drag a `respack.zip` file into the window and it will be automatically
  imported.

- *Where are the imported Respacks stored?*  
  (For now) They are not stored anywhere and only exist in-memory while the
  program is running which means you will have to re-import them every time
  (sorry :/).

## missing features
- Respack management
- Remote respack loading
- Editor
- Hue visuals options (blur, blur decay, colours palette)
- Program settings (fps, device management etc.)
- Animated images
- Image alignment
- Beatchar effects
  - Fade colour (`~`, `=`)
  - Slicing (`s`, `S`, `v`, `V`, `#`, `@`)
  - Trippy circles (`)`, `(`)
- Clickable source links

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
<images>
  <image name="image_src">
    <source>https://image.link/</source>
    <fullname>Image Fullname</fullname>
  </image>

  <image>
    ...
  </image>
</images>
```

* The `images.xml` MUST include a root `<images>` element
  * The `<images>` element MUST include one or more nested `<image>` elements
    * The `<image>` element MUST include a `name` attribute containing the image
      filename with no file extension
      * The file corresponding to the image filename MUST be included within the
        respack archive, either in the root of the archive or in a nested directory
    * The `<image>` element MUST include a `<source>` element
    * The `<image>` element MUST include a `<fullname>` element
    * The `<image>` element may OPTIONALLY include a `<source_other>` element
      * NOTE: Support to handle this element is currently not implemented
    * The `<image>` element may OPTIONALLY include a `<align>` element
      * NOTE: Support to handle this element is currently not implemented
    * The `<image>` element may OPTIONALLY include a `<frameDuration>` element
      to indicate this image is part of an animated set
      * NOTE: Support to handle this element and animated images is currently
        not implemented

## license and copyright

The source code for this software is licensed under the MIT license.  
See `LICENSE.txt` file in this repo for more information.

The file `hues.Game.Tests/Resources/Tracks/sample.mp3` is copyright of Kevin
MacLeod and is licensed under the Creative Commons Attribution License 4.0.

The following files are copyright of The 0x40 Hues Team:
- `hues.Game.Tests/Resources/Respacks/DefaultImages.zip`
- `hues.Game.Tests/Resources/Respacks/DefaultsHQ.zip`

