# Change Log
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [Unreleased]

### Added
- GIF support for the Unity.
- HDR support for the Unity.
- Unsafe APIs for decoding.
- Asynchronous APIs for decoding.

## [1.1.2] - 2022-07-30

### Changed
- Change the `AutoReferenced` option of assembly definitions to `true` in order to be able to refer assemblies from the Assembly-CSharp (the default assembly crated by the Unity).

## [1.1.1] - 2022-06-06

### Changed
- Change project settings (with no changes of APIs).

## [1.1.0] - 2022-06-06

### Changed
- Rename the `ImageConverter` to `TextureConverter`.
- Change the default value of the `linear` option to the `false`.

## [1.0.0] - 2022-06-04

### Added
- Decode an image binary to an `ImageResult` by the StbImageSharp.
- Decode an image binary to a `Texture2D`.
- Deploy demonstration codes.
