This Makefile does the following:

- Clones the apple sdk setup as a static library

- Creates binary for all architectures

- Combines them into one fat binary (you should ulimately use the libmParticle-Apple-SDK.a)

- Make sure you add the lib as a Native Reference to the Xamarin project, enable Smart Link, enable Force Load and add 
the following linker flags -lsqlite3 -lstdc++.6 -lc++
