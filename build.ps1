# Enter the release directory from the source.
cd DMR11/bin/release

# Create a directory for external libraries to avoid cluttering the
# main one.
mkdir lib

# Move all external libraries to the 'lib' directory.
mv *.dll lib

# Hide the config file.
attrib +h DMR11.exe.config

# Get the version of DMR11 to be used in the build output file.
$version = (Get-ChildItem DMR11.exe).VersionInfo.FileVersion
$version = $version.Substring(0, $version.LastIndexOf("."))

# The 7zip command used to create the build.
$7z_arg = "a -r DMR11-$version.7z DMR11.exe DMR11.exe.config lib/ ../../../licenses"

# Create a 7z release.
start "7z.exe" $7z_arg "/WAIT"

# Create a Zip release.
start "7z.exe" $7z_arg.Replace(".7z", ".zip") "/WAIT"

# Create a directory for compiled releases.
mkdir "../../../../releases"

# Move the newly-compiled releases to the releases directory.
mv "DMR11-$version.*" "../../../../releases"
