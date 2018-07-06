FROM microsoft/dotnet as builder

WORKDIR /builder
RUN echo "deb http://llvm.org/apt/trusty/ llvm-toolchain-trusty-3.9 main" | tee /etc/apt/sources.list.d/llvm.list
RUN wget -O - http://llvm.org/apt/llvm-snapshot.gpg.key | apt-key add -
RUN apt-get update
RUN apt-get install -y cmake clang-3.9 libicu57 libunwind8 uuid-dev libcurl4-openssl-dev zlib1g-dev libkrb5-dev
COPY SlamBot.csproj .
COPY nuget.config .
RUN dotnet restore -r linux-x64
COPY . .
RUN dotnet publish -r linux-x64 -c Release

FROM microsoft/dotnet:runtime-deps

WORKDIR /slambot
RUN apt-get update
RUN apt-get install -y libcurl3
COPY --from=builder /builder/bin/Release/netcoreapp2.1/linux-x64/native/SlamBot .
ENTRYPOINT /slambot/SlamBot