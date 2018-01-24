XBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
PROJECT_ROOT=./apple-static-library
PROJECT=$(PROJECT_ROOT)/mParticle-Apple-SDK.xcodeproj
TARGET=mParticle-Apple-SDK

all: clone lib$(TARGET).a cleanIntermediaries

clone: 
	git clone git@git.corp.mparticle.com:mParticle/apple-static-library.git

lib$(TARGET)-i386.a:
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphonesimulator/lib$(TARGET).a $@

lib$(TARGET)-armv7.a:
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch armv7 -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

lib$(TARGET)-arm64.a:
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch arm64 -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

lib$(TARGET).a: lib$(TARGET)-i386.a lib$(TARGET)-armv7.a lib$(TARGET)-arm64.a
	xcrun -sdk iphoneos lipo -create -output $@ $^

cleanIntermediaries:
	-rm -rf apple-static-library
	-rm lib$(TARGET)-armv7.a
	-rm lib$(TARGET)-arm64.a
	-rm lib$(TARGET)-i386.a

cleanAllBinary: 
	-rm -f *.a *.dll

clean: cleanIntermediaries cleanAllBinary